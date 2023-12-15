using System.Data.SQLite;

ReadData(CreateConnection());
//insertCostomer(CreateConnection());
RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection ()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source =mydb.db; Version = 3; New = True; Compress = True;");

    try
    {
        connection.Open();
        Console.WriteLine("DB found");
    }
    catch
    {
        Console.WriteLine("Db not found");
    }

    return connection;
}


static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * from customer";

    reader = command.ExecuteReader();
    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDOB = reader.GetString(3);

        Console.WriteLine($"{readerRowId} Full name: {readerStringFirstName} {readerStringLastName}; DOB: {readerStringDOB}");
    }
    myConnection.Close();
}  


static void insertCostomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, dob;

    Console.WriteLine("Enter  first name");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth(mm-dd-yyyy): ");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT into customer(firstname,lastname,dateofbirth)\r\nVALUES('{fName}','{lName}','{dob}')";

    int rowInterted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInterted} ");

    

    ReadData(myConnection);
}


static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string idToDelete;
    Console.WriteLine("Enter an id to deleate a costomer");
    idToDelete = Console.ReadLine();

    command=myConnection.CreateCommand();
    command.CommandText = $"DELETE from customer WHERE rowid = {idToDelete}";

    int rowRemove = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemove} was removed from the table costomer");

    ReadData(myConnection);
}