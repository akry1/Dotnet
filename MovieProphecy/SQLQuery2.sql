SELECT count(polarity),timestamp FROM details GROUP BY timestamp order by timestamp

SELECT DATEPART(mm,timestamp) as month, COUNT(polarity) 
FROM Details 
WHERE 


UPDATE
  details
SET
  timestamp = DATEADD(day, (ABS(CHECKSUM(NEWID())) % 65530), 0)

 SELECT * FROM details
WHERE timestamp BETWEEN '04/12/1988 12:00:00 AM' AND '05/25/2017 3:53:04 AM' 
AND DATEPART(dw, timestamp) >= 3 AND DATEPART(dw, timestamp) <= 7
 order by timestamp 

select DATEPART(mm, timestamp) mymonth, DATEPART(yy, timestamp)as MyYear,polarity, count(polarity) as count
from details
group by  DATEPART(yy, timestamp), polarity,DATEPART(mm, timestamp)
order by MyYear

select MyMonth ,MyYear,COUNT(polarity)as MyCount from mytemp
group by myyear,mymonth
order by MyYear

Insert into mytemp select DATEPART(yy, timestamp)as MyYear,polarity, count(polarity) as count
from details where polarity='pos'
group by  DATEPART(yy, timestamp),polarity
order by MyYear

select * from mytemp order by MyYear

SELECT * FROM Details WHERE userid='A'


drop table Mytemp


