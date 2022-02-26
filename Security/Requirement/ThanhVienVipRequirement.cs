using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Razor.Security.Requirements{
    public class ThanhVienVipRequirement : IAuthorizationRequirement{
        public int SoLuong{set;get;}
        public double SoTien{set;get;}
        public ThanhVienVipRequirement(int SoLuong, double SoTien){
            this.SoLuong = SoLuong;
            this.SoTien = SoTien;
        }


    }
}