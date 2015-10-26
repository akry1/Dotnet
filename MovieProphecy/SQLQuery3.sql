
DECLARE @min DateTime
DECLARE @max DateTime
DECLARE @range Int

drop table mytemp

CREATE TABLE mytemp
( 
	timeField DATETIME,
	polarity VARCHAR(MAX),
	polCount Int
)

SELECT @min = Min(timestamp) from details

SELECT @max=Max(timestamp) from details

SELECT @range= DATEDIFF(MINUTE , Min(timestamp), Max(timestamp))/20 from details

DECLARE @i Int SET @i=1
DECLARE @start DATETIME SET @start = @min
WHILE @i <= 20
BEGIN
INSERT INTO mytemp (timeField,polarity,polCount)
SELECT startTime,a.polarity, COUNT(a.polarity) as count
FROM (SELECT @start as startTime,polarity FROM DETAILS WHERE timestamp BETWEEN @start and DATEADD (MINUTE,@range, @start)) a 
GROUP BY startTime ,a.polarity
SET @start = DATEADD (MINUTE,@range, @start)
SET @i=@i+1
END

select * from mytemp

DECLARE @start DATETIME SET @start = '2012-12-31'
SELECT startTime,a.polarity, COUNT(a.polarity) FROM
(SELECT @start as startTime,polarity FROM DETAILS WHERE timestamp BETWEEN @start and DATEADD (MINUTE,300000, @start)) a
GROUP BY startTime, a.polarity


CONCAT(CONVERT (varchar,@start ), CONVERT(varchar,DATEADD (MINUTE,@range, @start) )) as timerange 