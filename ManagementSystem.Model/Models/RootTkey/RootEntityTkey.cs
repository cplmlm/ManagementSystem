using ManagementSystem.Model.Models.RootTkey.Interface;
using SqlSugar;
using System;

namespace ManagementSystem.Model
{
    public class RootEntityTkey<Tkey> where Tkey : IEquatable<Tkey>
    {
        /// <summary>
        /// Id
        /// 泛型主键Tkey
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true)]
        public Tkey Id { get; set; }

    }
}