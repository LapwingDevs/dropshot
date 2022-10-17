﻿namespace DropShot.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }


    public Address Address { get; set; }
    public int AddressId { get; set; }

    public Guid ApplicationUserId { get; set; }
}