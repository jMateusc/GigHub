﻿using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
        
        //Formata Data
        public DateTime GetDateTime()
        {
             return DateTime.Parse(string.Format("{0} {1}", Date, Time)); 
        }



        /*Referente a Layout*/
        public string Cabeçalho { get; set; }  //Cabeçalho Dinamico do Formulario (Add a Gig)
    }
}