namespace SP23.P01.Web.Models.Entities;

public class TrainStation
{
    public int Id { get; set; }

    public string Name
    {
        get { return Name; }
        set
        {
            if (Name.Length > 120)
            {
                throw new ArgumentException("Name must not be longer than 120 characters");
            }
        }
    }

    public string Address { get; set; }
}
