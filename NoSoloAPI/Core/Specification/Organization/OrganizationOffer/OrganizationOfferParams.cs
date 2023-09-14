﻿namespace Core.Specification.Organization.OrganizationOffer;

public class OrganizationOfferParams : BasicParams
{
    public Guid? OrganizationId { get; set; }
    public string? SortByAlphabetical { get; set; }
    public string? SortByDate { get; set; }
    
    // search by tags
    //
    
    private string _search;
    public string? Search
    { 
        get => _search;
        set => _search = value.ToLower();
    }
}