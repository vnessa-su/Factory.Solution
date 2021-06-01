# Dr. Sillystringz's Factory - Machine Repairs

#### Web app that keeps track of repair engineers and machines that need to be maintained.

#### By Vanessa Su

## Description

MVC pattern web app that is used to keep track of engineers and machines in a factory. Engineers have machines that they are qualified to repair, which is displayed on their details page. Machines will also display what engineers are qualified to repair them on their details page. This association is stored in a MySQL database, with a many-to-many relationship between the Engineer and Machine tables. Associations can be added or removed from the machine pages or the engineer pages.

## User Story

- See a splash page listing all engineers and machines
- Click on an engineer or machine to see its details
- For engineers, see a list of machines they are certified to repair
- For machines, see a lit of engineers certified to repair them
- Add new machines/engineers without needing to associate them with a machine/engineer
- Add and remove machines/engineers from specific machines/engineers

## Technologies Used

- C#
- ASP.NET&#8203; Core
- Razor
- Entity Framework Core
- MySQL
- VS Code

## Setup/Installation Requirements

### Prerequisites

- [MySQL](https://www.mysql.com/)
- [.NET](https://dotnet.microsoft.com/)
- A text editor like [VS Code](https://code.visualstudio.com/)

### Installation

1. Clone the repository: `git clone https://github.com/vnessa-su/Factory.Solution.git`
2. Navigate to the `/Factory.Solution` directory
3. Open with your preferred text editor to view the code base

- #### Database Setup

1. Navigate to the `/Factory` directory
2. Create appsettings.json file: `touch appsettings.json`
3. Open appsettings.json in a text editor and add in:

```
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=<port number>;Database=vanessa_su;Uid=root;Pwd=<password>;"
  }
}
```

- Replace `<port number>` with the port number the server is running on, default is usually 3306
- Replace `<password>` with your MySQL password

5. Save the file and go back to the terminal
6. Run `dotnet ef database update`

- #### Run the Program

1. Navigate to the `/Factory` directory
2. Run `dotnet restore`
3. Run `dotnet build`
4. Start the program with `dotnet run`
5. Open http://localhost:5000/ in your preferred browser

## Known Bugs

_No known bugs_

## Contact Information

For any questions or comments, please reach out through GitHub.

## License

[MIT License](license)

Copyright (c) 2021 Vanessa Su
