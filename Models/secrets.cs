using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DojoSecrets.Models
{
    public class secrets{

        public secrets(){
            like = new List<likes>();
        }
        [Key]
        public int id{get;set;}

        public int usersid{get;set;}
        public users user{get;set;}
        public List<likes> like {get;set;}
        public string secret{get;set;}
        public DateTime created_at{get;set;}
        public DateTime updated_at{get;set;}

    }
}
