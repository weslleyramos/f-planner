 
declare @periodInit varchar(max) set @periodInit = '2017-08-01';	 
declare @periodEnd  varchar(max) set @periodEnd  = '2017-08-31';	
 
select 
	* 
from [litz].[dbo].[transaction]
where 
	datediff(day, convert(date, @periodInit), [date]) >= 0 and
	datediff(day, convert(date, @periodEnd), [date]) <= 0
	order by [date]  asc;

select 
	sum(amount) as 'Entradas'
from [litz].[dbo].[transaction]
where 
	datediff(day, convert(date, @periodInit), [date]) >= 0 and
	datediff(day, convert(date, @periodEnd), [date]) <= 0 and 
	Amount > 0

select 
	sum(amount) as 'Despesas'
from [litz].[dbo].[transaction]
where 
	datediff(day, convert(date, @periodInit), [date]) >= 0 and
	datediff(day, convert(date, @periodEnd), [date]) <= 0 and
	Amount < 0
 
	