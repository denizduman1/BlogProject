﻿using BlogProject.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Shared.Utilities.Results.Abstract
{
    public interface IDataResult<out T> : IResult
        where T: class,IEntity,new()
    {
        public T Data { get; } // Categori, List of Category
    }
}