﻿namespace NoSolo.Contracts.Dtos.Organizations.Offers;

public class NewOrganizationOfferDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Tags { get; set; }
}