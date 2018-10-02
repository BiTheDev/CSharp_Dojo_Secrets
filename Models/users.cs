using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DojoSecrets.Models
{
    public class users{

        public users(){
            Userlike = new List<likes>();
            UserSecret = new List<secrets>();
        }
        
        [Key]
        public int id{get;set;}
        

        [Required]
        public string first_name{get; set;}

        [Required]
        public string last_name{get; set;}


        [Required]
        public string email {get; set;}

        [Required]
        public string password {get; set;}

        public DateTime created_at{get;set;}
        public DateTime updated_at{get; set;}

        public List<likes> Userlike{get;set;}

        public List<secrets> UserSecret{get;set;}


    }
}