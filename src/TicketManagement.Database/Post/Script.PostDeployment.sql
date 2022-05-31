--- Venue
insert into dbo.Venue
	values 
		('First venue', 'First venue address', '123 45 678 90 12'),
		('Second venue', 'Second venue address', '544 38 665 90 64'),
		('Thirst venue', 'Thirst venue address', '953 69 678 30 17')

--- Layout
insert into dbo.Layout
	values 
		(1, 'First layout'),
		(1, 'Second layout'),
		(2, 'First layout'),
		(2, 'Second layout'),
		(2, 'Thirst layout'),
		(3, 'First layout'),
		(3, 'Second layout')

--- Area
insert into dbo.Area
	values 
		(1, 'First area of first layout', 1, 1),
		(1, 'Second area of first layout', 1, 1),
		(2, 'First area of second layout', 4, 4)
		
--- Event
insert into dbo.Event
	values 
		('Sider-Man II', '2022-05-30 00:00:00', 'Spider-Man II Movie', 2),
		('Venom', '2022-06-08 00:00:00', 'Venom Movie', 1),
		('Soccer', '2022-05-30 00:00:00', 'CSKA-TSMOKY', 1)

--- Seat
insert into dbo.Seat
	values
		(1, 1, 1),
		(1, 1, 2),
		(1, 2, 3),
		(1, 2, 2),
		(2, 1, 1),
		(1, 2, 1)
		
--- EventArea
insert into dbo.EventArea
	values 
		(1, 'Cinema Hall #2', 2, 1, CAST(5.20 AS Decimal(18, 2))),
		(2, 'Cinema Hall #1', 1, 1, CAST(5.40 AS Decimal(18, 2))),
		(2, 'Stadion #1', 16, 16, CAST(15.80 AS Decimal(18, 2)))
		
--- EventSeat
insert into dbo.EventSeat
	values 
		(1, 1, 1, 0),
		(1, 1, 2, 0),
		(1, 2, 1, 1),
		(1, 2, 2, 0),
		(1, 2, 3, 1),
		(2, 1, 1, 0),
		(2, 1, 2, 1),
		(2, 2, 1, 0),
		(2, 2, 2, 1)
	