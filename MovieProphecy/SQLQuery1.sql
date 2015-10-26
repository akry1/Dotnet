select polarity, count(polarity) from details where userid like 'A%' group by polarity
select Count(*) from details where userid like 'A%' and polarity = 'neg'

update details set polarity = 'neg' where userid like 'am%'

update details set polarity = 'pos' where polarity = 'neg'
