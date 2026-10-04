# Modernization Analysis

Scope: `ProjectManagementSystem/` (Blazor Server, .NET 6, EF Core 6, SQL Server, MudBlazor 6, ASP.NET Identity).
About 2.6k lines of hand-written code, 12 entities, 17 migrations, no tests, no CI.

## 1. What it does today

- Hierarchy: School → Department → Programme → Course / Student.
- People: Student, Supervisor, Coordinator. Each is linked 1:1 to an Identity `User`. Roles: Administrator, Coordinator, Supervisor.
- Student projects: abstract, keywords, PDF upload and download, and grouping of students under a supervisor.
- Assessment: a 6-criterion presentation evaluation sheet (4 criteria graded 0–10, 2 graded 0–5, total /50).
- Dashboard and per-entity CRUD pages, built on MudBlazor tables.

## 2. Problems found (highest impact first)

### Security
| # | Finding | Where |
|---|---------|-------|
| S1 | Admin `test@admin.com` / `test1234` is created on every startup, in every environment. | `Program.cs` `SeedAdmin` |
| S2 | Uploaded PDFs go to `wwwroot/files/`, so they are served as public static files with no auth. Anyone with a URL can read any student's work. Filenames are random, but that is obscurity, not access control. | `ProjectService.UploadPdf` |
| S3 | Authorization is UI-only. Roles appear only in `<AuthorizeView>` markup and `[Authorize]` page attributes. Services have no checks, and `Assessments.razor` has no role restriction, so any logged-in user can mark any project. | `Pages/*`, `Services/*` |
| S4 | Upload accepts any extension up to 20 MB. There is no content-type or magic-byte check, and no antivirus hook. | `ProjectPage.razor` `UploadFiles` |
| S5 | A machine-specific SQL Server connection string is committed. Secrets belong in user-secrets or environment variables. | `appsettings.Development.json` |
| S6 | 12 student PDFs (about 18 MB) are committed to git, which is likely personal or academic data. | `wwwroot/files/` |
| S7 | `AddUser` ignores the `IdentityResult` from `CreateAsync` and `AddToRoleAsync`. A failed password policy still returns a "user id". | `UserService` |

### Correctness and robustness
- `ServiceBase<T>` wraps every write in `try { } catch { return false; }`. Callers cannot tell a validation error from a deadlock, and nothing is logged.
- `UploadPdf` fires `Task.Run(() => data.WriteTo(fileStream))` and never awaits, flushes or disposes it. It saves `FileName` to the database before the file is on disk, so a crash leaves a dangling reference. The download path also leaks its `FileStream`.
- `ProjectAssessment` is an owned collection with no key of its own (only `ProjectId`), so an assessment cannot be identified or edited individually. `MarkedBy` is a free-text string rather than a link to a user.
- `Course` and `Group` are not declared as `DbSet`s in the context and are only reachable through navigations.
- `GroupService.GetGroupDetails` loads all students and supervisors purely so EF fixes up navigation properties. This is an N+1-style workaround.
- The `DbContext` is registered `Transient` and the data access is synchronous (`SaveChanges`, `Find`, `ToList`). In Blazor Server this blocks circuit threads. The correct pattern is `IDbContextFactory` with async calls.
- No nullable reference types, no unique indexes (for example `StudentIDNumber`), no input validation attributes beyond one `[StringLength]`.
- Modelling redundancy: `Student.SupervisorId` and `Group.SupervisorId` both exist and can disagree. A `Project` has both `StudentId` and `GroupId`, so group projects have no clean ownership.
- Typos baked into the schema: `ProjectDescritpion`, plus migrations named `Initial` to `Initial11`, `hhh` and `250`.

### Maintainability
- .NET 6 is out of support. EF Core 6, MudBlazor 6 and `Microsoft.AspNetCore.Identity` 2.2.0 (a legacy package that is redundant with the framework) are all behind.
- Template leftovers: `Counter`, `FetchData`, `WeatherForecast*`, `SurveyPrompt`, an unused `BlazorInputFile` package, and large blocks of commented-out code.
- Nine near-identical `*Page.razor` CRUD pages (about 120–220 lines each) and nine near-identical services. Almost all of this is boilerplate.
- No tests, no CI, no real README, no Docker or deployment story.

## 3. Functional and process improvements

The current system digitises records. It does not yet digitise the process. Proposed workflow, and what each step needs:

1. **Project lifecycle states**: Proposal → Approved/Rejected → In progress → Submitted → Under assessment → Graded → Archived. Currently there is no state at all.
2. **Proposal approval**: a student submits a proposal and the supervisor or coordinator approves it with comments. This needs a review-comment thread per project.
3. **Milestones and deadlines**: configurable per academic year, with e-mail or in-app reminders and a late-submission flag.
4. **Versioned submissions**: keep every upload (draft, final) with a timestamp and hash. Today a new upload overwrites `FileName`.
5. **Multi-marker assessment**: several assessors each submit an independent sheet. The system computes the average and flags large disagreement for moderation. Rubrics become data (criteria and weights per programme) instead of hard-coded enums.
6. **Supervisor workload and allocation**: capacity per supervisor, an allocation view, and auto-suggest based on keywords.
7. **Reporting and export**: marks to CSV/Excel, per-programme and per-year dashboards, and printable evaluation sheets (PDF).
8. **Audit log**: who changed which mark and when. This is essential for grade disputes.
9. **Academic-year scoping**: everything should hang off an `AcademicYear` so old cohorts are read-only and archived.
10. **Repository search**: full-text search over abstracts and keywords, as an institutional archive of past projects, with originality checking via an external service.
11. **Self-service onboarding**: bulk import of students and supervisors from CSV, with invite links, instead of admin-created accounts with passwords.
12. **Notifications**: e-mail on proposal decisions, new comments, and marks released.

## 4. Technology stack options

| Option | Stack | Pros | Cons | Fit |
|--------|-------|------|------|-----|
| **A (recommended)** | **.NET 10 LTS**, Blazor Web App (interactive server, with auto render mode where useful), EF Core 10, **PostgreSQL** (or keep SQL Server), ASP.NET Identity, MudBlazor 8 | Smallest migration risk: the models, Identity data and Razor markup port almost directly. Same language and skills. First-class LTS support. | Blazor Server needs a persistent connection per user. Larger memory footprint than a stateless web app. | Best |
| B | ASP.NET Core Web API + **React/Next.js** (TypeScript), PostgreSQL | Clean API that mobile apps or other integrations can reuse. Large UI talent pool. | Two codebases and a full UI rewrite. More moving parts for a small team. | Good if a mobile app or a wider integration is planned |
| C | **Django** (Python) + HTMX, PostgreSQL | The built-in admin replaces roughly nine CRUD pages. Auth, permissions, migrations, forms and i18n are included. Fastest to a working system. | Different language. Rich interactive UI (inline-editable assessment grids) needs more work. | Best if the team prefers Python or wants minimal code |
| D | **Laravel** + Livewire/Inertia, MySQL/PostgreSQL | Cheap, ubiquitous PHP hosting, which is common in university IT. Filament gives admin CRUD quickly. | Different language. Smaller typed-domain safety. | Good for low-cost hosting |
| E | NestJS + React (TypeScript end to end), PostgreSQL | One language across the stack. | Weakest batteries-included story for auth and admin. More code to write. | Only if the team is already TypeScript-first |

**Recommendation: Option A.** The domain is small and well understood, and the business logic is simple CRUD plus a scoring sheet. The real wins are the security fixes, the data-model cleanup and the workflow features in §3. None of these need a different language, so a rewrite would spend effort without fixing any of them. Choose C only if there is a firm preference for Python or for the Django admin.

## 5. Suggested target architecture (Option A)

```
src/
  Spromas.Domain/          entities, enums, rubric & scoring logic (no dependencies)
  Spromas.Application/     use-cases, interfaces, authorization rules, validation (FluentValidation)
  Spromas.Infrastructure/  EF Core (IDbContextFactory), file storage, e-mail, seed
  Spromas.Web/             Blazor Web App, Identity UI, generic CRUD components
tests/
  Spromas.Domain.Tests/    scoring + state-machine unit tests
  Spromas.Web.Tests/       integration tests (WebApplicationFactory + Testcontainers)
```

Key design changes:
- Replace `ServiceBase<T>` with async repositories or services that return a `Result<T>` (success, error code and message) and log exceptions.
- Add policy-based authorization (`RequireRole`, plus resource checks such as "a supervisor may only see their own students") enforced in the application layer, not only in markup.
- Store files behind an `IFileStorage` abstraction (local disk outside `wwwroot`, or S3/Azure Blob). Serve them through an authorized endpoint with streaming and content-type validation. Keep versions and hashes in the database.
- One reusable generic `CrudPage<T>` / `DataGrid` component to replace the nine duplicated pages.
- Model fixes: an explicit `Assessment` entity (own key, `AssessorId` FK, `RubricId`), `AcademicYear`, `ProjectStatus` enum, a unique index on `StudentIDNumber`, `<Nullable>enable`, and a single source of truth for the student–supervisor relationship (group-level or student-level, not both).
- Configuration: user-secrets and environment variables for connection strings. Seed the admin only in Development, and generate a random password for production first-run.
- Operations: Dockerfile and docker-compose (app and Postgres), GitHub Actions (build, test, format check), health checks, structured logging (Serilog), and EF migrations squashed to one clean `InitialCreate`.

## 6. Phased plan

| Phase | Work | Size |
|-------|------|------|
| 0 | Remove the seeded admin, secrets and committed PDFs (and purge them from history). Add a README, CI and a basic test project. | 1–2 days |
| 1 | Upgrade to .NET 10, EF Core 10, MudBlazor 8. Enable nullable. Remove template leftovers. | 2–3 days |
| 2 | Async data access, `Result<T>`, authorization policies, and the secure file store with versioning. | 1 week |
| 3 | Data-model cleanup and a squashed migration, with a data migration script for existing records. | 3–5 days |
| 4 | Workflow: statuses, proposals, comments, milestones, notifications. | 2–3 weeks |
| 5 | Rubric-driven multi-marker assessment, audit log, reports and export, academic-year scoping. | 2–3 weeks |
