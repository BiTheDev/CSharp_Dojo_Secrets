using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DojoSecrets.Models
{
    public class likes{

 
     [Key]
    public int id{get;set;}
    public int usersid{get;set;}
    public int secretsid{get;set;}

    public int like{get;set;}
    public users user{get;set;}
    public secrets secret{get;set;}


    }
        
}