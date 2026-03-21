markdown# Library Book Tracking API
A simple HTTP API for tracking library books, members, and checkouts. Built with ASP.NET Core (.NET 10) using an EF Core in-memory database.
## Endpoints
| Method | Route                              | Description                                      |
|--------|-------------------------------------|--------------------------------------------------|
| GET    | `/api/books`                        | List all books with availability status           |
| GET    | `/api/members/{memberId}/books`     | List all books currently checked out by a member  |
| POST   | `/api/checkouts`                    | Check out a book to a member                      |
## Running the App
### Visual Studio 2026
Open `Library.sln`, set `Library.Api` as the startup project, and hit **F5**. The browser should automatically open the Scalar UI at `/scalar/v1`, you can use it to test all endpoints directly without needing Postman or similar tools.
### VS Code / CLI
```bash
cd Library/Library.Api
dotnet run
```
## Manually Testing with Scalar
Once the app runs, it should open the browser at `/scalar/v1` which you can use to test out endpoints without the hassle of setting up Postman etc.

<img width="951" height="688" alt="image" src="https://github.com/user-attachments/assets/cd9394c6-c57e-46a4-83ed-2eba9c14be50" />

Select one of the endpoints in the side menu shown, press **Test Request**, fill in any required route params / body, then press **Send**.

e.g. `GET /api/books` should show the books that the app auto seeds on startup:

<img width="2095" height="1571" alt="image" src="https://github.com/user-attachments/assets/b0bf9d73-8993-4813-89fe-b0529c127ee2" />

It is recommended to call this get all books endpoint first to check the member IDs / book ISBNs and copy them when testing the other two endpoints.

e.g. check which books are checked out by Alice (who is seeded on startup with id `a1111111-1111-1111-1111-111111111111`):

<img width="1041" height="518" alt="image" src="https://github.com/user-attachments/assets/cf195584-bf19-4783-979b-d79a34d0447b" />

Then open the URL shown in the terminal (e.g. `https://localhost:<port>/scalar/v1`) to access the Scalar API UI.
## Running the Tests
### Visual Studio 2026
Open **Test Explorer** (`Ctrl+E, T`) and click **Run All**.
### VS Code / CLI
```bash
cd Library/Library.Tests
dotnet test
```
## Assumptions
- In-memory database with pre-seeded books, members, and a couple of checkouts, no persistence across restarts.
- ISBNs are kept short (e.g. `"1"`, `"2"`) for easier manual testing.
- No return-book endpoint was implemented as the spec only asked for checkout.
- Test coverage prioritises the happy paths and key integration flows given the time constr
