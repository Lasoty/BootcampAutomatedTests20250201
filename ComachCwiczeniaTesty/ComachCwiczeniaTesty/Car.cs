using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComachCwiczeniaTesty;

public class Car
{
    public Car()
    {
        Marka = "Ford";
    }

    public Car(string marka)
    {
        Marka = marka;
    }

    public string Marka { get; set; }
}

public class Bus : Car
{
    public Bus() : base("Audi")
    {
        Model = "Focus";
    }

    public string Model { get; set; }

}
