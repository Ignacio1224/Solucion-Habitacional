﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Clases
{
    [Table("ViviendaNueva")]
    public class ViviendaNueva:Vivienda
    {
        #region properties

        #endregion


        #region Metodos

        public override string ToString()
        {
            return base.ToString();
        }

        public override string ReturnType()
        {
            return "ViviendaNueva";
        }

        public override decimal ReturnContribucion()
        {
            return 0;
        }

        #endregion
    }
}

