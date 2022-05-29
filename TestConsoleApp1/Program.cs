using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;

// See https://aka.ms/new-console-template for more information
var bll = new AreaService();
var bllV = new VenueService();

bll.Delete(new Area(id: 1024, layoutId: 2, description: "e_!___!_!!__w", coordX: 4, coordY: 6));

#pragma warning disable S1125
#pragma warning disable SA1408
#pragma warning disable S1067
#pragma warning disable CS0162
#pragma warning disable S1764
#pragma warning disable SA1512
#pragma warning disable SA1515
#pragma warning disable S125
//bllV.Insert(new Venue(id: 2, description: "Fiveth venue", address: "sasas", phone: "sasas66"));
if (true & true & true)
{
    Console.WriteLine(Environment.NewLine + "TRUE");
}
#pragma warning restore S125
#pragma warning restore SA1515
#pragma warning restore SA1512
#pragma warning restore S1764
#pragma warning restore CS0162
#pragma warning restore S1067
#pragma warning restore SA1408
#pragma warning restore S1125

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
