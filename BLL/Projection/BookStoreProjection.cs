﻿using BLL.Abstract.Projection;

namespace BLL.Projection
{
  public class BookStoreProjection : AbstractProjection<int>
  {
    public string Name { get; set; }
    public string OIB { get; set; }
    public double DelayPricePerDay { get; set; }
    public string Email { get; set; }
  }
}