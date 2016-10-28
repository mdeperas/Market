using System;
using System.ComponentModel.DataAnnotations;

namespace MarketSimulator.Repository.Models
{
	public class EntityModelBase
	{
		[Display(Name = "Id: ")]
		[Key]
		public string Id { get; set; }

		public EntityModelBase()
		{
			this.Id = Guid.NewGuid().ToString();
		}
	}
}