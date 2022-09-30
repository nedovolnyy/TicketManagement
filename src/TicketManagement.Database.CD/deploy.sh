chmod +x ./TicketManagement.Database.CD
connectionString=$(get_octopusvariable "ConnectionStrings:DefaultConnection")
./TicketManagement.Database.CD "$connectionString"
