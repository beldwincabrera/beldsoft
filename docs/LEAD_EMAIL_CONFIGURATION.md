# Lead notification configuration

The consultation form creates a **Lead** and prepares a plain-text email
notification for `beldwin@beldsoft.com`. The prospect's email address is set as
`Reply-To`, so replying to the notification starts a direct conversation with
the prospect.

The Lead is written to the `pending` directory before notification handoff. A
successful local file drop or a Microsoft Graph `202 Accepted` response moves
the Lead record into its monthly `accepted` directory. This state means the
configured channel accepted the notification, not that a recipient's mailbox
received it. A notification failure leaves the recoverable Lead record in
`pending`.

## Non-production: local email drop

Development, Staging, Testing, and every other non-Production environment never
send real email. They write a standard `.eml` notification to:

```text
src/Beldsoft.Web/App_Data/lead-email-drop
```

The directory is created on the first successful submission and is excluded
from source control. Each file can be opened in a desktop mail client or
inspected as a MIME-formatted text file. The UTF-8 message body is base64
encoded.

Override the drop location when needed:

```text
Leads__LocalEmailDropDirectory=C:\some\private\lead-email-drop
```

Relative paths are resolved from the web project's content root. Absolute paths
are accepted. The application rejects filesystem roots and any directory inside
`wwwroot` for both Lead storage and the email drop, because those files contain
personal information.

## Production: Microsoft Graph

Production uses Microsoft Graph app-only authentication and calls:

```text
POST https://graph.microsoft.com/v1.0/users/{sender}/sendMail
```

Microsoft Graph mail requires the configured sender to have an **Exchange
Online mailbox** in the Microsoft Entra tenant. Graph cannot send from a Google
Workspace-only mailbox. The recipient can still be
`beldwin@beldsoft.com`.

### Microsoft Entra setup

1. Register a confidential application in Microsoft Entra ID.
2. Create a client credential for the application.
3. Choose one authorization model:
   - **Recommended, mailbox-scoped:** register the application's service
     principal in Exchange Online and assign the `Application Mail.Send` role
     with a management scope containing only the dedicated sender mailbox.
   - **Tenant-wide:** grant Microsoft Graph **Application** permission
     `Mail.Send` in Entra ID and grant tenant administrator consent.
4. For the mailbox-scoped model, do **not** also retain an unscoped Entra
   `Mail.Send` application permission. Entra and Exchange RBAC grants are
   additive, so the unscoped grant would preserve access to every mailbox.
5. Validate the authorization against the dedicated Exchange Online sender
   mailbox before enabling Production.

Microsoft documents the required endpoint and permission in
[user: sendMail](https://learn.microsoft.com/graph/api/user-sendmail?view=graph-rest-1.0),
the app-only flow in
[OAuth 2.0 client credentials](https://learn.microsoft.com/entra/identity-platform/v2-oauth2-client-creds-grant-flow),
and mailbox scoping in
[RBAC for Applications in Exchange Online](https://learn.microsoft.com/exchange/permissions-exo/application-rbac).

### Required production environment variables

Configure these as protected server or hosting-platform settings. Do not commit
the client secret.

| Environment variable | Purpose |
|---|---|
| `Leads__Graph__TenantId` | Microsoft Entra tenant ID or verified tenant domain |
| `Leads__Graph__ClientId` | Application registration client ID |
| `Leads__Graph__ClientSecret` | Protected application credential |
| `Leads__Graph__SenderUserId` | Exchange Online sender mailbox UPN or user ID |
| `Leads__Graph__TimeoutSeconds` | Overall token and Graph request timeout; defaults to `30` |
| `Leads__RecipientAddress` | Notification recipient; defaults to `beldwin@beldsoft.com` |
| `Leads__StorageDirectory` | Durable, private Lead-record storage |

Production startup fails immediately when the Graph configuration is
incomplete. Restart the application after changing any environment variable.

The Graph client uses the `https://graph.microsoft.com/.default` scope and
stores no access tokens or credentials in application files or logs. A
successful response means Microsoft Graph accepted the message for processing;
it is not a final-delivery receipt.
