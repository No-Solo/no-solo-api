﻿using Core.Enums;

namespace Core.Entities;

public class Request<T, U> : BaseEntity where T : BaseEntity where U : BaseEntity
{
    public T TEntity { get; set; }
    public Guid TEntityId { get; set; }

    public StatusEnum Status { get; set; }

    public U UEntity { get; set; }
    public Guid UEntityId { get; set; }
}