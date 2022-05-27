using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;

// See https://aka.ms/new-console-template for more information
var bll = new AreaService();
var bllV = new VenueService();

bll.Update(new Area(id: 2, layoutId: 2, description: "!!!!!!!!!32w", coordX: 4, coordY: 6));

List<Venue> l = bllV.GetAll().ToList();

foreach (var area in l)
{
    Console.WriteLine(area.Description.ToString());
}

int i = 3;
while (i > 0)
{
    Console.WriteLine(bll.GetById(i).Description.ToString());
    i--;
}
