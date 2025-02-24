using Model;
using ViewModel;

CityDB cdb = new();
CityList cList = cdb.SelectAll();
foreach (City c in cList)
    Console.WriteLine(c.CityName);

#region

PersonDB pdb = new();
PersonList pList = pdb.SelectAll();
foreach (Person c in pList)
    Console.WriteLine(c.FirstName);
#endregion