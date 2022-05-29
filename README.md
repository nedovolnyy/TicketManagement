
# Task-1
BusinessLogic Layer
DataAccess Layer 

# Structure
* BusinessLogic Layer (https://github.com/EPAM-Gomel-NET-Lab/ArtsiomKrot/tree/master/src/TicketManagement.BusinessLogic/)
* DataAccess Layer (https://github.com/EPAM-Gomel-NET-Lab/ArtsiomKrot/tree/master/src/TicketManagement.DataAccess/)
* Unit Tests (https://github.com/EPAM-Gomel-NET-Lab/ArtsiomKrot/tree/master/test/TicketManagement.UnitTests/)
* Integration Tests (https://github.com/EPAM-Gomel-NET-Lab/ArtsiomKrot/tree/master/test/TicketManagement.IntegrationTests/)

# Usage
* Create new instance of necessary bll.service of entity and use it.
Example:
```
var example = new AreaService();
```
* Use necessary method of Insert, Update, Delete, GetById, GetAll
Example:
```
example.Insert(Entity entity);
example.Update(Entity entity);
example.Delete(Entity entity); or example.Delete(int entity.id);
example.GetById(int entity.id);
example.GetAll();
```

# Steps how to check

# Credentials

