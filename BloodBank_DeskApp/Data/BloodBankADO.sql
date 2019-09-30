

use master
create database bloodBankDB
go

use bloodBankDB
create table Users
(
UserName nvarchar(30) not null,
Password nvarchar(30) not null,
IsActive bit not null
);

use bloodBankDB
create table Staff
(
StaffID int primary key identity,
Name nvarchar(40) not null,
Designation nvarchar(20) not null,
Gender nvarchar(10) not null,
StaffAge int not null,
StaffBG nvarchar(5) not null,
StaffAddress nvarchar(50) not null,
StaffPhone nvarchar(17) not null,
StaffEmail nvarchar(30),
JoiningDate datetime default getdate(),
ResineingDate datetime,
IsActive bit not null
)
go

use bloodBankDB
create table Donor
(
DonorID int primary key identity,
DonorName nvarchar(40) not null,
Gender nvarchar(10) not null,
DonorAge int,
DonorAddress nvarchar(50),
DonorPhone nvarchar(17) not null,
DonorBG nvarchar(5) not null,
DonationDate datetime default getdate(),
DonatedQty int,
StaffID int foreign key references Staff(StaffID)
)
go

use bloodBankDB
create table Patient
(
PatientID int primary key identity,
PatientName nvarchar(40) not null,
PatientGender nvarchar(10) not null,
PatientAge int,
PatientAddress nvarchar(50),
PatientPhone nvarchar(17) not null,
RequestedBG nvarchar(5) not null,
ReceivedDate datetime default getdate(),
ReceivedQty int,
StaffID int foreign key references Staff(StaffID)
)
go

use bloodBankDB
create table Test
(
EntryID int primary key identity,
DonorID int foreign key references Donor(DonorID),
TestDate datetime default getdate(),
TestedBloodGroup nvarchar(5) not null,
HIV bit not null,
HepatitisB bit not null,
HepatitisC bit not null,
HTLV bit not null,
StaffID int foreign key references Staff(StaffID)
)
go

use bloodBankDB
create table Store
(
EntryID int foreign key references Test(EntryID),
BloodGroup nvarchar(4),
BloodBagQty int,
StoredDate datetime,
ExpiryDate datetime
)
go

use bloodBankDB
create table TotalBlood
(
BloodGroup nvarchar (20) null,
BloodBagQty int default 0
)
Go

-----------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Users(UserName,Password,IsActive) VALUES('Mahfuz','Mahfuz123',1)
go

insert into Staff values ('Asif','Receptionist','Male',24,'A+','Halishahar,Ctg','01811547895','asif@gmail.com',getdate(),' ',1),
						 ('Arif','Manager','Male',36,'AB-','Dewanhat,Ctg','01784578498','Arif@yahoo.com',getdate(),' ',1),
						 ('Akash','Doctor','Male',32,'O+','Potenga,Ctg','01711458795','Akash@outlook.com',getdate(),' ',1),
						 ('Amir','Tecnical Supervisor','Male',28,'B+','Khulshi,Ctg','01854784589',' ',getdate(),' ',1),
						 ('Fahmida','Nurse','Female',24,'B+','Khulshi,Ctg','01745876582',' ',getdate(),' ',1)
go

insert into Donor values('Akib','Male',24,'Ctg','01512457812','B+',getdate(),1,1),
						('Foysal','Male',27,'Ctg','01715784853','O+',getdate(),1,1),
						('Belal','Male',27,'Ctg','01614578458','B+',getdate(),1,1),
						('Zahed','Male',26,'Ctg','01924578495','B+',getdate(),1,1)
go

insert into Patient values('Tushar','Male',25,'Ctg','01567845895','O+',getdate(),1,1),
						  ('Nazmun','Female',27,'Ctg','01961245867','O+',getdate(),1,1),
						  ('Sakhawat','Male',27,'Ctg','01536458752','O+',getdate(),1,1),
						  ('Sohel','Male',28,'Ctg','01754685264','B+',getdate(),1,1)
go

insert into Test values(1,getdate(),'B+',1,1,1,1,3),
					   (1,getdate(),'A+',1,0,1,1,3),
					   (1,getdate(),'AB+',1,1,0,1,3),
					   (1,getdate(),'B-',1,1,1,1,3)
go

Insert into TotalBlood(BloodGroup) values
('A+'),('A-'),('B+'),('B-'),('AB+'),('AB-'),('O+'),('O-')
GO

------------------------------------------------------------------------------------------------------------------------
use bloodBankDB
go

create proc sp_TestAndStore
(
@donorid int,
@testedbloodgroup nvarchar(5),
@hiv bit,
@hepatitisb bit,
@hepatitisc bit,
@htlv bit,
@staffid int
)
as
begin
Set nocount on
	begin try
	declare @donorqty int
	set @donorqty=(select DonatedQty from Donor where DonorID=@donorid)
	declare @expdate datetime
				
					begin
						insert into Test(DonorID,TestDate,TestedBloodGroup,HIV,HepatitisB,HepatitisC,HTLV,StaffID)
						values (@donorid,getdate(),@testedbloodgroup,@hiv,@hepatitisb,@hepatitisc,@htlv,@staffid)						
						Print 'Data Inserted Successfully' 
							if(@hiv=0 and @hepatitisb=0 and @hepatitisc=0 and @htlv=0)
								begin
									insert into Store(EntryID,BloodGroup,BloodBagQty,StoredDate,ExpiryDate)
									values (@@identity,@testedbloodgroup,@donorqty,getdate(),dateadd(MM,4,getdate()))
									Print 'Data Inserted Successfully'
									if(@testedbloodgroup = 'A+')
									begin
									Update TotalBlood set BloodBagQty = BloodBagQty + @donorqty where BloodGroup = 'A+'
									end

									if(@testedbloodgroup = 'A-')
									begin
									Update TotalBlood set BloodBagQty = BloodBagQty + @donorqty Where BloodGroup = 'A-'
									end

									if(@testedbloodgroup = 'B+')
									begin
									Update TotalBlood set BloodBagQty = BloodBagQty + @donorqty Where BloodGroup = 'B+'
									end

									if(@testedbloodgroup = 'B-')
									begin
									Update TotalBlood set BloodBagQty = BloodBagQty + @donorqty Where BloodGroup = 'B-'
									end

									if(@testedbloodgroup = 'AB+')
									begin
									Update TotalBlood set BloodBagQty = BloodBagQty + @donorqty Where BloodGroup = 'AB+'
									end

									if(@testedbloodgroup = 'AB-')
									begin
									Update TotalBlood set BloodBagQty = BloodBagQty + @donorqty Where BloodGroup = 'AB-'
									end

									if(@testedbloodgroup = 'O+')
									begin
									Update TotalBlood set BloodBagQty = BloodBagQty + @donorqty Where BloodGroup = 'O+'
									end

									if(@testedbloodgroup = 'O-')
									begin
									Update TotalBlood set BloodBagQty = BloodBagQty + @donorqty Where BloodGroup = 'O-'
									end
								end
					end				
	end try

	begin catch
		Print 'Blood Is Not Safe'
	end catch
end
go

exec sp_TestAndStore 2,'B+',0,0,0,0,1
go



----drop proc sp_TestAndStore
----go

------------------------------------------------------------------------------------------------------------------------

use bloodBankDB
go

create proc sp_PatientAndStore
(
@patientname nvarchar(40),
@patientgender nvarchar(10),
@patientage int,
@patientaddress nvarchar(50),
@patientphone nvarchar(17),
@requestedbg nvarchar(5),
@receiveddate datetime,
@receivedqty int,
@staffid int
)
as
begin
Set nocount on
	begin try				
			begin
				insert into Patient(PatientName,PatientGender,PatientAge,PatientAddress,PatientPhone,RequestedBG,ReceivedDate,ReceivedQty,StaffID)
				values (@patientname,@patientgender,@patientage,@patientaddress,@patientphone,@requestedbg,getdate(),@receivedqty,@staffid)						
				Print 'Data Inserted Successfully'
						begin
							if(@requestedbg = 'A+')
							begin
							Update TotalBlood set BloodBagQty = BloodBagQty - @receivedqty where BloodGroup = 'A+'
							end

							if(@requestedbg = 'A-')
							begin
							Update TotalBlood set BloodBagQty = BloodBagQty - @receivedqty Where BloodGroup = 'A-'
							end

							if(@requestedbg = 'B+')
							begin
							Update TotalBlood set BloodBagQty = BloodBagQty - @receivedqty Where BloodGroup = 'B+'
							end

							if(@requestedbg = 'B-')
							begin
							Update TotalBlood set BloodBagQty = BloodBagQty - @receivedqty Where BloodGroup = 'B-'
							end

							if(@requestedbg = 'AB+')
							begin
							Update TotalBlood set BloodBagQty = BloodBagQty - @receivedqty Where BloodGroup = 'AB+'
							end

							if(@requestedbg = 'AB-')
							begin
							Update TotalBlood set BloodBagQty = BloodBagQty - @receivedqty Where BloodGroup = 'AB-'
							end

							if(@requestedbg = 'O+')
							begin
							Update TotalBlood set BloodBagQty = BloodBagQty - @receivedqty Where BloodGroup = 'O+'
							end

							if(@requestedbg = 'O-')
							begin
							Update TotalBlood set BloodBagQty = BloodBagQty - @receivedqty Where BloodGroup = 'O-'
							end
						end
			end				
	end try

	begin catch
		Print 'Unsuccessfull'
	end catch
end
go


----drop proc sp_PatientAndStore
----go

exec sp_PatientAndStore 'A','Male',20,'Ctg','01874859687','A+','2/2/2019',1,2
go


select * from Users
select * from Staff
select * from Donor
select * from Patient
select * from Test
select * from Store
select * from TotalBlood