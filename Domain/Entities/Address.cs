namespace Domain.Entities;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
}