﻿using BaseProjectMvc_Net8.Models;

namespace BaseProjectMvc_Net8.Services
{
    public interface IOrderService
    {
        ItemModel GetSubtotal(ItemModel itemModel, HttpContext? httpContext);
    }
}
