﻿CREATE PROCEDURE [dbo].DataForHistogram
	@MovieName VARCHAR(MAX)
AS
	DECLARE @min DateTime
	DECLARE @max DateTime
	DECLARE @range Int

	DELETE FROM mytemp

	SELECT @min = Min(tweet_time ) from MP_Table2 WHERE movie_name = @MovieName

	SELECT @max=Max(tweet_time) from MP_Table2 WHERE movie_name = @MovieName

	SET @range= DATEDIFF(MINUTE , @min, @max)/20 

	DECLARE @i Int SET @i=1
	DECLARE @start DATETIME SET @start = @min
	WHILE @i <= 20
	BEGIN
		INSERT INTO mytemp (timeField,polarity,polCount)
		SELECT startTime,a.polarity, COUNT(a.polarity) as count
		FROM (SELECT @start as startTime,polarity FROM MP_Table2 WHERE movie_name = @MovieName AND tweet_time BETWEEN @start AND DATEADD (MINUTE,@range, @start)) a 
		GROUP BY startTime ,a.polarity
		SET @start = DATEADD (MINUTE,@range, @start)
		SET @i=@i+1
	END
RETURN 0