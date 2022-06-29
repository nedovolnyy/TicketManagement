--- Venue
insert into dbo.Venue
	values 
		('First venue', 'Description of first venue', 'First venue address', '123 45 678 90 12'),
		('Second venue', 'Description of second venue', 'Second venue address', '544 38 665 90 64'),
		('Third venue', 'Description of third venue', 'Third venue address', '953 69 678 30 17'),
		('Fourth venue', 'Description of fourth venue', 'Fourth venue address', '374 38 665 63 64'),
		('Fifth venue', 'Description of fifth venue', 'Five venue address', '953 69 678 30 17'),
		('Sixth venue', 'Description of sixth venue', 'Sixth venue address', '544 38 665 90 64'),
		('Seventh venue', 'Description of seventh venue', 'Seventh venue address', '953 69 638 30 17'),
		('Eighth venue', 'Description of eighth venue', 'Eighth venue address', '374 38 665 63 64'),
		('Ninth venue', 'Description of ninth venue', 'Ninth venue address', '953 69 678 40 17'),
		('Tenth venue', 'Description of tenth venue', 'Tenth venue address', '544 38 655 90 64'),
		('Eleventh venue', 'Description of eleventh venue', 'Eleventh venue address', '953 69 678 30 17'),
		('Twelfth venue', 'Description of twelfth venue', 'Twelfth venue address', '374 38 665 63 64')

--- Layout
insert into dbo.Layout
	values 
		('First layout', 1, 'Description of first layout'),
		('Second layout', 2, 'Description of second layout'),
		('Third layout', 3, 'Description of third layout'),
		('Fourth layout', 4, 'Description of fourth layout'),
		('Fifth layout', 5, 'Description of fifth layout'),
		('Sixth layout', 6, 'Description of sixth layout'),
		('Seventh layout', 7, 'Description of seventh layout'),
		('Eighth layout', 8, 'Description of eighth layout'),
		('Ninth layout', 9, 'Description of ninth layout'),
		('Tenth layout', 10, 'Description of tenth layout'),
		('Eleventh layout', 11, 'Description of eleventh layout'),
		('Twelfth layout', 12, 'Description of twelfth layout')

--- Area
insert into dbo.Area
	values 
		(1, 'First area of first layout', 3, 1),
		(2, 'First area of second layout', 4, 2),
		(3, 'First area of third layout', 5, 1),
		(4, 'First area of fourth layout', 4, 9),
		(5, 'First area of fifth layout', 7, 8),
		(6, 'First area of sixth layout', 6, 2),
		(7, 'First area of seventh layout', 1, 7),
		(8, 'First area of eighth layout', 9, 5),
		(9, 'First area of ninth layout', 3, 1),
		(10, 'First area of tenth layout', 7, 2),
		(11, 'First area of eleventh layout', 8, 1),
		(12, 'First area of twelfth layout', 6, 9)
				
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
		(2, 2, 1),
		(2, 2, 2),
		(3, 1, 1),
		(3, 1, 2),
		(3, 2, 1),
		(3, 2, 2),
		(4, 1, 1),
		(4, 1, 2),
		(4, 2, 1),
		(4, 2, 2),
		(5, 1, 1),
		(5, 1, 2),
		(5, 2, 1),
		(5, 2, 2),
		(6, 1, 1),
		(6, 1, 2),
		(6, 1, 3),
		(6, 2, 1),
		(6, 2, 2),
		(6, 2, 3),
		(7, 1, 1),
		(7, 1, 2),
		(7, 2, 1),
		(7, 2, 2),
		(8, 1, 1),
		(8, 1, 2),
		(8, 2, 1),
		(8, 2, 2),
		(9, 1, 1),
		(9, 1, 2),
		(9, 2, 1),
		(9, 2, 2),
		(10, 1, 1),
		(10, 1, 2),
		(10, 2, 1),
		(10, 2, 2)

--- Event
insert into dbo.Event
	values 
		('Sider-Man II', '2023-05-30 00:00:00', 'Spider-Man II Movie', 1, '2023-05-30 00:45:00'),
		('Venom', '2023-06-08 00:00:00', 'Venom Movie', 2, '2023-06-08 01:00:00'),
		('Soccer', '2023-05-30 00:00:00', 'CSKA-TSMOKY', 3, '2023-05-30 01:30:00'),
		('Venom', '2023-06-08 00:00:00', 'Venom Movie', 4, '2023-06-08 01:00:00'),
		('Soccer', '2023-05-30 00:00:00', 'CSKA-TSMOKY', 5, '2023-05-30 01:30:00'),
		('Venom', '2023-06-08 00:00:00', 'Venom Movie', 6, '2023-06-08 01:00:00'),
		('Soccer', '2023-05-30 00:00:00', 'CSKA-TSMOKY', 7, '2023-05-30 01:30:00'),
		('Venom', '2023-06-08 00:00:00', 'Venom Movie', 8, '2023-06-08 01:00:00'),
		('Soccer', '2023-05-30 00:00:00', 'CSKA-TSMOKY', 9, '2023-05-30 01:30:00'),
		('Soccer', '2023-05-30 00:00:00', 'CSKA-TSMOKY', 10, '2023-05-30 01:30:00')

--- EventArea
insert into dbo.EventArea
	values 
		(1, 'Cinema Hall #2', 2, 1, CAST(5.20 AS Decimal(18, 2))),
		(2, 'Cinema Hall #1', 1, 1, CAST(5.40 AS Decimal(18, 2))),
		(3, 'Stadion #1', 16, 16, CAST(15.80 AS Decimal(18, 2))),
		(4, 'Cinema Hall #1', 1, 1, CAST(5.40 AS Decimal(18, 2))),
		(5, 'Stadion #1', 16, 16, CAST(15.80 AS Decimal(18, 2))),
		(6, 'Cinema Hall #1', 1, 1, CAST(5.40 AS Decimal(18, 2))),
		(7, 'Stadion #1', 16, 16, CAST(15.80 AS Decimal(18, 2))),
		(8, 'Cinema Hall #1', 1, 1, CAST(5.40 AS Decimal(18, 2))),
		(9, 'Stadion #1', 16, 16, CAST(15.80 AS Decimal(18, 2))),
		(10, 'Stadion #1', 16, 16, CAST(15.80 AS Decimal(18, 2)))
		
--- EventSeat
insert into dbo.EventSeat
	values 
		(1, 1, 1, 1),
		(1, 1, 2, 0),
		(1, 1, 3, 1),
		(1, 2, 1, 1),
		(1, 2, 2, 0),
		(1, 2, 3, 1),
		(2, 1, 1, 1),
		(2, 1, 2, 1),
		(2, 2, 1, 0),
		(2, 2, 2, 1),
		(3, 1, 1, 0),
		(3, 1, 2, 1),
		(3, 2, 1, 0),
		(3, 2, 2, 1),
		(4, 1, 1, 0),
		(4, 1, 2, 1),
		(4, 2, 1, 0),
		(4, 2, 2, 1),
		(5, 1, 1, 1),
		(5, 1, 2, 1),
		(5, 2, 1, 0),
		(5, 2, 2, 1),
		(6, 1, 1, 1),
		(6, 1, 2, 0),
		(6, 1, 3, 1),
		(6, 2, 1, 1),
		(6, 2, 2, 0),
		(6, 2, 3, 1),
		(7, 1, 1, 1),
		(7, 1, 2, 1),
		(7, 2, 1, 0),
		(7, 2, 2, 1),
		(8, 1, 1, 0),
		(8, 1, 2, 1),
		(8, 2, 1, 0),
		(8, 2, 2, 1),
		(9, 1, 1, 0),
		(9, 1, 2, 1),
		(9, 2, 1, 0),
		(9, 2, 2, 1),
		(10, 1, 1, 1),
		(10, 1, 2, 1),
		(10, 2, 1, 0),
		(10, 2, 2, 1)
	