﻿using EcommerceCore.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            IsDeleted = false;
        }
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public CommonStatus Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
