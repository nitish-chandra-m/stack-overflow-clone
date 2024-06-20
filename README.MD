# Stack Overflow Clone using ASP.NET Core MVC and SQL Server
---
Simulating StackOverflow search using StackOverflow data upto 2010. 

Demo Link: <a href="https://stack-overflow-clone.azurewebsites.net/" target="_blank">https://stack-overflow-clone.azurewebsites.net/</a>

## Key Features:
1. ASP.NET Core MVC application hosted on Azure App Service powered by SQLServer database also hosted on Azure.
1. Relevant search results returned in order of relevance to search query using Full-text search and CONTAINSTABLE/RANK constructs.
1. Offset-based Pagination with progressive loading
1. Authentication and Authorization implemented with ASP.NET Identity
1. EF Core Reverse Engineering to build models based on the pre-existing database schema
1. Periodic server push notifications using SignalR
1. Optimized database performance with relevant indexes added.

[Data Source](https://www.brentozar.com/archive/2015/10/how-to-download-the-stack-overflow-database-via-bittorrent/)