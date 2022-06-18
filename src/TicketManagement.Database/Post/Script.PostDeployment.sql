--- Venue
insert into dbo.Venue
	values 
		('First venue', 'Description first venue', 'First venue address', '123 45 678 90 12'),
		('Second venue', 'Description second venue', 'Second venue address', '544 38 665 90 64'),
		('Thirst venue', 'Description thirst venue', 'Thirst venue address', '953 69 678 30 17')

--- Layout
insert into dbo.Layout
	values 
		('First layout', 1, 'Description first layout'),
		('Second layout', 1, 'Description second layout'),
		('First layout', 2, 'Description first layout'),
		('Second layout', 2, 'Description second layout'),
		('Thirst layout', 2, 'Description thirst layout'),
		('First layout', 3, 'Description first layout'),
		('Second layout', 3, 'Description second layout')

--- Area
insert into dbo.Area
	values 
		(1, 'First area of first layout', 1, 1),
		(1, 'Second area of first layout', 1, 1),
		(2, 'First area of second layout', 4, 4)
		
--- Event
insert into dbo.Event
	values 
		('Sider-Man II', '2023-05-30 00:00:00', 'Spider-Man II Movie', 2, '2023-05-30 00:45:00'),
		('Venom', '2023-06-08 00:00:00', 'Venom Movie', 1, '2023-06-08 01:00:00'),
		('Soccer', '2023-05-30 00:00:00', 'CSKA-TSMOKY', 1, '2023-05-30 01:30:00')

--- Seat
insert into dbo.Seat
	values
		(1, 1, 1),
		(1, 1, 2),
		(1, 1, 3),
		(1, 2, 1),
		(1, 2, 2),
		(1, 2, 3),
		(2, 1, 1),
		(2, 1, 2),
		(2, 1, 3),
		(2, 2, 1),
		(2, 2, 2),
		(2, 2, 3),
		(3, 1, 1),
		(3, 1, 2),
		(3, 2, 1),
		(3, 2, 2)
		
--- EventArea
insert into dbo.EventArea
	values 
		(1, 'Cinema Hall #2', 2, 1, CAST(5.20 AS Decimal(18, 2))),
		(2, 'Cinema Hall #1', 1, 1, CAST(5.40 AS Decimal(18, 2))),
		(2, 'Stadion #1', 16, 16, CAST(15.80 AS Decimal(18, 2)))
		
--- EventSeat
insert into dbo.EventSeat
	values 
		(1, 1, 1, 1),
		(1, 1, 2, 1),
		(1, 1, 3, 1),
		(1, 2, 1, 1),
		(1, 2, 2, 1),
		(1, 2, 3, 1),
		(2, 1, 1, 0),
		(2, 1, 2, 1),
		(2, 1, 3, 1),
		(2, 2, 1, 0),
		(2, 2, 2, 1),
		(2, 2, 3, 0),
		(3, 1, 1, 1),
		(3, 1, 2, 1),
		(3, 2, 1, 1),
		(3, 2, 2, 1)
	