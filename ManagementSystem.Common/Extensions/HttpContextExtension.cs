﻿using System;
using Microsoft.AspNetCore.Http;

namespace ManagementSystem.Common.Extensions;

public static class HttpContextExtension
{
	public static ISession GetSession(this HttpContext context)
	{
		try
		{
			return context.Session;
		}
		catch (Exception)
		{
			return default;
		}
	}
}