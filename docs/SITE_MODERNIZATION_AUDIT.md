# Beldsoft Website Modernization Audit

Date: July 26, 2026  
Branch: `develop`

## Executive summary

The original site was a broad IT-agency template rather than a credible Beldsoft consulting website. It exposed unsupported people, clients, project results, testimonials, prices, products, office details, dates, service guarantees, and social profiles. Navigation promoted multiple template homepages, duplicate contact pages, a shop, cart, checkout, registration, team profiles, testimonials, and generic blog content. The contact form always reported success while only writing personal information to the console.

The modernized site now positions Beldsoft as a practical software engineering and technology consulting partner for small and medium-sized organizations. The information architecture is smaller, claims are supportable, calls to action consistently lead to a consultation request, and the visual system is responsive and accessible.

## Page and route inventory

| Route | Original state | Implemented state |
|---|---|---|
| `/` | Long template homepage with fabricated counters, clients, team, testimonials, blog, and unrelated effects | Outcome-led home with verified services, business challenges, four-step approach, and consultation CTA |
| `/about` | Template sections repeating unsupported team and testimonials | Purpose, positioning, and six working principles without invented history or people |
| `/services` | Six generic IT cards with unsupported capabilities | Six differentiated, outcome-oriented consulting services |
| `/services/{slug}` | Generic image and sparse feature list | Unique metadata, clear value proposition, capabilities, engagement model, and contextual CTA |
| `/projects` | Six fabricated clients and projects | “How we help” use cases clearly labeled as representative solution types |
| `/pricing` | Unsupported SaaS subscription tiers and guarantees | Honest engagement models and factors that shape a proposal |
| `/faq` | Unsupported years, project counts, technologies, staffing, and response promises | Seven factual answers covering fit, process, integration, AI, pricing, and support |
| `/contact` | Fabricated address/phone/hours; form always returned success | Accessible consultation form with server persistence, validation, duplicate prevention, rate limiting, honeypot, consent, attribution, and real success/failure states |
| `/privacy-policy` | Generic legal text with an unsupported 2025 date | Contact-data-specific notice plus clearly marked legal decisions |
| `/terms` | Unsupported New York governing-law statement | Website terms without invented jurisdiction, plus clearly marked legal decisions |
| `/404` and `/Error` | Template error content and decorative imagery | Focused, no-index recovery pages |
| `/blog*` | Placeholder and lorem-ipsum articles with invented authors/comments | Redirects to current “How we help” content; no fabricated content remains public |
| `/shop*`, `/cart`, `/checkout`, `/register` | Unsupported products, prices, stock, reviews, and checkout | Redirects to services |
| `/team*`, `/testimonials` | Invented staff, contact information, clients, and outcomes | Redirects to About |
| `/contact-2` | Duplicate form with fabricated business details | Redirects to Contact |
| `/coming-soon` | Template utility page | Redirects to Home |
| `/projects/{slug}` | Fabricated project detail | Redirects to the representative use-case page |

## Before-and-after audit

### Current problems found

1. The positioning alternated among IT agency, digital-product shop, SaaS vendor, staffing-style team, and software consultancy.
2. The primary navigation exposed template variants and irrelevant commerce/account routes.
3. Unsupported facts included a New York address, `555` phone numbers, social profiles, named employees, named clients, testimonials, results, project volumes, years in business, prices, stock quantities, service-level promises, and technology claims.
4. Calls to action were inconsistent (`Get Started`, `Talk to Sales`, `Contact Us`, shop actions) and often pointed to the wrong page.
5. Core pages lacked descriptions, canonical links, Open Graph tags, and social metadata.
6. Sitemap and robots files were missing.
7. Images and template effects introduced large payloads, decorative noise, empty alternative text, and layout risk.
8. The viewport disabled zoom.
9. The header depended on template JavaScript for its mobile menu and contained placeholder links.
10. The form lacked organization, service interest, contact preference, consent, server-side validation, spam controls, duplicate protection, reliable storage, and honest failure handling.
11. The contact service logged submitted personal information and always returned `true`.
12. Legal content invented a governing jurisdiction and did not accurately describe the contact workflow.
13. The site loaded multiple legacy scripts and style sheets on every route.

### Implemented improvements

- Consolidated navigation around Home, Services, About, How We Help, FAQ, and Request a Consultation.
- Rewrote all active public pages in plain business language for SME decision-makers.
- Replaced generic IT services with six supportable software-consulting capabilities.
- Removed unsupported claims from public data services and retired fabricated public routes.
- Replaced project claims with explicitly labeled representative solution types.
- Replaced fixed-price marketing tiers with discovery, defined delivery, and ongoing engineering models.
- Added a single shared responsive visual system with consistent typography, spacing, cards, buttons, focus states, and calls to action.
- Removed legacy jQuery, Bootstrap template, animation, carousel, cursor, preloader, search, color-switcher, and social-gallery dependencies from the runtime shell.
- Added responsive behavior for mobile, tablet, desktop, reduced motion, accessible zoom, skip navigation, semantic landmarks, keyboard focus, touch-sized controls, and a native Blazor mobile menu.
- Added shared SEO metadata, canonical URLs, Open Graph and Twitter metadata, `robots.txt`, and `sitemap.xml`.
- Added no-index behavior to error and redirect states.
- Added server-side contact persistence, validation, a honeypot, rate limiting, duplicate protection, disabled/loading state, attribution, consent, success confirmation, failure handling, and PII-safe logging.

## Content and positioning strategy

**Position:** Beldsoft is a practical software engineering and technology consulting partner for small and medium-sized organizations.

**Primary audience:** Owners, founders, operations leaders, and technology leaders dealing with manual workflows, disconnected systems, aging applications, delivery risk, or a need for experienced technical direction.

**Primary CTA:** Request a consultation.

**Content model:** Lead with the business problem and expected operational benefit, explain relevant engineering capability without unnecessary jargon, make scope-dependent claims explicit, and direct the visitor to one next step.

**Approved service catalog:**

1. Custom Software Development
2. Software Modernization
3. Architecture & Technical Leadership
4. APIs & Systems Integration
5. Workflow Automation & Applied AI
6. Cloud, DevOps & Reliability

## Contact workflow

1. Blazor `EditForm` validates required fields and data formats with data annotations.
2. The user supplies name, organization, email, optional phone, service interest, contact preference, project description, and privacy acknowledgement.
3. A hidden honeypot rejects simple bot submissions.
4. The server applies the same model validation, limits attempts per normalized-email hash, and rejects duplicate message fingerprints within the configured window.
5. Accepted requests are written as individual JSON records under a private `pending` directory.
6. Non-production environments atomically write a `.eml` notification to the local drop folder; Production submits the notification through Microsoft Graph.
7. Successful notification handoff moves the Lead record into a monthly `accepted` directory. This means the local drop or Graph accepted it, not that a recipient's mailbox received it. Failures leave the recoverable record in `pending`.
8. The service logs only generated Lead IDs and delivery-channel status. It does not log Lead details, access tokens, or credentials.

### Production configuration

Configuration section: `Leads`

| Setting | Default | Production guidance |
|---|---|---|
| `StorageDirectory` | `App_Data/leads` | Mount an encrypted, durable, access-controlled volume outside the deployed application package |
| `LocalEmailDropDirectory` | `App_Data/lead-email-drop` | Non-production-only private `.eml` outbox; never place beneath `wwwroot` |
| `DuplicateWindowMinutes` | `10` | Adjust only after observing legitimate retry behavior |
| `MaximumSubmissionsPerHour` | `5` | Tune with infrastructure-level rate limiting and monitoring |
| `RecipientAddress` | `beldwin@beldsoft.com` | Internal mailbox that receives every accepted Lead |
| `Graph:TenantId`, `Graph:ClientId` | empty | Configure the Microsoft Entra application in the production environment |
| `Graph:ClientSecret` | empty | Supply only through protected production secret configuration |
| `Graph:SenderUserId` | empty | Exchange Online mailbox UPN or user ID used by Graph `sendMail` |

Only the Production environment can send a real notification. Every other environment writes a private `.eml` file locally. Production requires an Exchange Online sender mailbox, app-only Microsoft Graph authorization, protected credentials, and preferably mailbox-scoped Exchange Online RBAC. Do not retain an unscoped Entra `Mail.Send` application grant when using scoped Exchange RBAC because the permission models are additive. Configure the documented `Leads__*` environment variables and define retention/deletion policies before launch.

## SEO by page

- **Home:** Software engineering and technology consulting for SMEs.
- **About:** Practical consulting approach and working principles.
- **Services:** Overview of the six supported capabilities.
- **Service details:** Unique title, description, canonical route, capabilities, and contextual consultation CTA.
- **How We Help:** Business-problem use cases without unsupported case-study claims.
- **Engagement Models:** Natural search intent around consulting scope and pricing.
- **FAQ:** Specific engagement, integration, modernization, AI, and support questions.
- **Contact:** Consultation-request intent and complete business inquiry context.
- **Privacy / Terms:** Unique metadata and accurate website/contact handling.
- **Errors / redirects:** Explicitly excluded from indexing.

## Accessibility and performance

- Targets WCAG 2.2 AA with semantic landmarks, skip link, visible focus, keyboard-operable navigation, accessible form labels/messages, non-color-only status, zoom support, reduced-motion handling, and minimum touch target sizing.
- Uses fluid type and layouts across 320, 375, 390, 768, 1024, 1280, and 1440+ widths.
- Removes the runtime dependency on the original template’s large CSS/JS stack and decorative image carousels.
- Uses width and height attributes on brand images to reduce layout shift.
- Uses CSS-generated hero artwork instead of a large decorative image payload.

## Validation results

| Check | Result |
|---|---|
| Repository and `develop` branch | Confirmed |
| Route inventory | Completed |
| Unsupported public facts scan | Removed from active public content |
| Internal navigation review | Completed |
| SEO metadata / sitemap / robots review | Completed |
| Secret and PII logging review | Completed |
| Production build | **Not run: .NET SDK is not installed in the execution environment** |
| Automated tests | No test project existed; not run because .NET SDK is unavailable |
| Browser/rendered breakpoint QA | Requires a runnable .NET environment |

Required verification in a .NET 9 environment:

```bash
dotnet restore Beldsoft.sln
dotnet format Beldsoft.sln --verify-no-changes
dotnet build Beldsoft.sln --configuration Release
dotnet test Beldsoft.sln --configuration Release
```

Then render every sitemap route at the target widths, verify the browser console and server logs, test contact success/failure/spam/duplicate scenarios on durable storage, and run an automated accessibility and Core Web Vitals audit.

## Remaining decisions, prioritized

1. **Production Lead email and retention:** Provision the Exchange Online sender mailbox and mailbox-scoped Exchange RBAC role without an additional unscoped Entra `Mail.Send` grant, configure the documented `Leads__Graph__*` environment variables, mount durable storage, and define retention/deletion.
2. **Legal review:** Confirm legal entity, jurisdiction, service providers, privacy rights, retention, governing law, venue, warranties, and liability language.
3. **Verified trust content:** Supply approved founder/leadership information, verifiable credentials, client references, or case studies before adding them.
4. **Company facts:** Confirm operating location, service area, phone number, business hours, and official social profiles before publishing them.
5. **Analytics and consent:** Select a privacy-appropriate analytics platform and consent approach only if measurement requirements justify it.
6. **Case studies / insights:** Add these only after source material, attribution, review ownership, and an ongoing publishing process exist.
