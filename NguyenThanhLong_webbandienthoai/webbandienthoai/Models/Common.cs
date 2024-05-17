using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webbandienthoai.Models
{
    public class Common
    {
        public static string html_danhgia(int DanhGia)
        {
            var str = "";
            if (DanhGia == 0)
            {
                str = @"<i class='fa fa-star-o empty'></i>
                <i class='fa fa-star-o empty'></i>
                <i class='fa fa-star-o empty'></i>
                <i class='fa fa-star-o empty'></i>
                <i class='fa fa-star-o empty'></i>";
            }
            if (DanhGia == 1)
            {
                str = @"<i class='fa fa-star'></i>
                <i class='fa fa-star-o empty'></i>
                <i class='fa fa-star-o empty'></i>
                <i class='fa fa-star-o empty'></i>
                <i class='fa fa-star-o empty'></i>";
            }
            if (DanhGia == 2)
            {
                str = @"<i class='fa fa-star'></i>
                <i class='fa fa-star'></i>
                <i class='fa fa-star-o empty'></i>
                <i class='fa fa-star-o empty'></i>
                <i class='fa fa-star-o empty'></i>";
            }
            if (DanhGia == 3)
            {
                str = @"<i class='fa fa-star'></i>
                <i class='fa fa-star'></i>
                <i class='fa fa-star'></i>
                <i class='fa fa-star-o empty'></i>
                <i class='fa fa-star-o empty'></i>";
            }
            if (DanhGia == 4)
            {
                str = @"<i class='fa fa-star'></i>
                <i class='fa fa-star'></i>
                <i class='fa fa-star'></i>
                <i class='fa fa-star'></i>
                <i class='fa fa-star-o empty'></i>";
            }
            if (DanhGia == 5)
            {
                str = @"<i class='fa fa-star'></i>
                <i class='fa fa-star'></i>
                <i class='fa fa-star'></i>
                <i class='fa fa-star'></i>
                <i class='fa fa-star'></i>";
            }
            return str;
        }

    }
}