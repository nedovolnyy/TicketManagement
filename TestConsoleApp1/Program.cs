using TicketManagement.BusinessLogic.Services;
using TicketManagement.Common.Entities;

// See https://aka.ms/new-console-template for more information
var bll = new AreaService();

bll.Update(new Area(id: 1028, layoutId: 1, description: "e_!_!223!32w", coordX: 4, coordY: 6));

#pragma warning disable S1125
#pragma warning disable SA1408
#pragma warning disable S1067
#pragma warning disable CS0162
#pragma warning disable S1764
#pragma warning disable SA1512
#pragma warning disable SA1515
#pragma warning disable S125
// var bllV = new LayoutService();
// bllV.Insert(new Layout(id: 2, venueId: 2, description: "Secnd layout"));
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

int i = 3;
while (i > 0)
{
    Console.WriteLine(bll.GetById(i).Description.ToString());
    i--;
}
