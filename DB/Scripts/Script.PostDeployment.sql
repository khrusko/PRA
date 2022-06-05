/*
Post-Deployment Script Template              
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.    
 Use SQLCMD syntax to include a file in the post-deployment script.      
 Example:      :r .\myfile.sql                
 Use SQLCMD syntax to reference a variable in the post-deployment script.    
 Example:      :setvar TableName MyTable              
               SELECT * FROM [$(TableName)]          
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'admin@admin.com') BEGIN
  ALTER TABLE [Users] NOCHECK CONSTRAINT [FK_Users_CreatedBy]
  ALTER TABLE [Users] NOCHECK CONSTRAINT [FK_Users_UpdatedBy]
  ALTER TABLE [Users] NOCHECK CONSTRAINT [FK_Users_DeletedBy]

  DECLARE @Password AS nvarchar(50) = 'Pa$$w0rd'

  INSERT INTO [dbo].[Users] 
  (
    [CreatedBy], 
    [UpdatedBy], 
    [UserID], 
    [FName], 
    [LName], 
    [Email], 
    [PasswordHash], 
    [IsAdmin],
    [ConfirmationIsApproved],
    [ConfirmationDate]
  )
  VALUES 
  (
    1, 
    1, 
    'D2205150001', 
    'admin', 
    'admin', 
    'admin@admin.com', 
    CONVERT(nvarchar(512), HASHBYTES('SHA2_512', @Password), 2), 
    1,
    1,
    GETDATE()
  )

  ALTER TABLE [Users] CHECK CONSTRAINT [FK_Users_CreatedBy]
  ALTER TABLE [Users] CHECK CONSTRAINT [FK_Users_UpdatedBy]
  ALTER TABLE [Users] CHECK CONSTRAINT [FK_Users_DeletedBy]
END
GO

-- USERS [KUPCI]

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'azver@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Andrija', 'Zver', 'azver@gmail.com', 'azverPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'mkos@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Marija', 'Kos', 'mkos@gmail.com', 'mkosPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'isimic@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Ilija', 'Šimić', 'isimic@gmail.com', 'isimicPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'sambrus@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Stjepan', 'Ambruš', 'sambrus@gmail.com', 'sambrusPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID


END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'dtopolnjak@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Dario', 'Topolnjak', 'dtopolnjak@gmail.com', 'dtopolnjakPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'fkocijan@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Filip', 'Kocijan', 'fkocijan@gmail.com', 'fkocijanPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'ljuric@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Lovro', 'Jurić', 'ljuric@gmail.com', 'ljuricPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'nkresoje@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Nikola', 'Kresoje', 'nkresoje@gmail.com', 'nkresojePass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'astunkovic@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Arjan', 'Stunković', 'astunkovic@gmail.com', 'astunkovicPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'msprajc@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Mihael', 'Šprajc', 'msprajc@gmail.com', 'msprajcPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'bpetko@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Borna', 'Petko', 'bpetko@gmail.com', 'bpetkoPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'jturkovic@gmail.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Julia', 'Turković', 'jturkovic@gmail.com', 'jturkovicPass', 0
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

-- USERS [DJELATNICI]

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'mmatic@racunarstvo.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Mihael', 'Matić', 'mmatic@racunarstvo.com', 'mmaticPass', 1
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'jtatare@racunarstvo.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Jan', 'Tatarević', 'jtatare@racunarstvo.com', 'jtatarePass', 1
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'khrusko@racunarstvo.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Karlo', 'Hruškovec', 'khrusko@racunarstvo.com', 'khruskoPass', 1
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Users] WHERE [Email] = 'mvujnov@racunarstvo.com') BEGIN
  DECLARE @ID AS int
  EXECUTE @ID = [dbo].[UserRegister] 'Mario', 'Vujnović', 'mvujnov@racunarstvo.com', 'mvujnovPass', 1
  
  DECLARE @GUID AS uniqueidentifier
  SELECT ALL @GUID = [ConfirmationGUID] FROM [dbo].[Users] WHERE [ID] = @ID

  EXECUTE [dbo].[UserConfirmRegistration] @GUID
END
GO

-- AUTHORS

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = 'Vjenceslav' AND [LName] = 'Novak' AND [BirthDate] = '18590911') BEGIN
  EXECUTE [dbo].[AuthorCreate] 'Vjenceslav', 'Novak', '18590911', NULL, 'Novak, književnik realizma, širem je čitateljstvu najpoznatiji po svome epohalnom romanu „Posljednji Stipančići“, koji je dio srednjoškolske lektire. No, on je bio mnogo više od toga. Štoviše, za svoga života bio je najugledniji književnik realizma te je nazivan i hrvatskim Balzacom, no unatoč tome, živio je u velikoj neimaštini. Pisao je romane, feljtone, pripovijetke i kritike, a u svemu tome okrenuo se malom čovjeku. Značajan je po tome što je prvi u hrvatsku književnost unio malog čovjeka, sa svim njegovim problemima, slabostima i nadanjima. Prikazivao je pravu sliku hrvatskog siromaštva, propadanja patricija, ali i nezavidan položaj žena. Prisjetimo se samo Lucije Stipančić koja je imala neusporedivo lošiji tretman kod svog oca (ako se to može nazvati ocem) nego njezin stariji brat. Osim „Posljednjih Stipančića“, svakako preporučujem i bildungsroman „Tito Dorčić“, „Dva svijeta“ i pripovijetke „Iz velegradskog podzemlja“ te „Nezasitnost i bijeda“.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = 'Miroslav' AND [LName] = 'Krleža' AND [BirthDate] = '18930707') BEGIN
  EXECUTE [dbo].[AuthorCreate] 'Miroslav', 'Krleža', '18930707', NULL, 'Sažeti Krležino stvaralaštvo u svega nekoliko rečenica nije nimalo jednostavno. Njegove se drame smatraju najboljima u hrvatskoj književnosti, Glembajevi su postali svojevrsno nacionalno blago, po njemu je nazvan današnji Leksikografski zavod te postoji i velika enciklopedija posvećena njegovom životu i djelu nazvana „Krležijana“. Jedan od najboljih književnika svih vremena hrvatske književnosti pisao je pjesme, novele, drame, romane, putopise, eseje i polemike. Progovarao je o sukobu čovjeka i Boga, o besmislenosti rata i o njegovom pravom licu koje deformira ljudskost, o društvenoj nepravdi, staležima i složenim međuljudskim odnosima. Uz „Glembajeve“ svakako zavirite  barem u zbirku novela „Hrvatski bog Mars,“, prvi moderni hrvatski roman „Povratak Filipa Latinovicza“ i dijalektalnu liriku u „Baladama Petrice Kerempuha“.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = 'Dragutin' AND [LName] = 'Tadijanović' AND [BirthDate] = '19051104') BEGIN
  EXECUTE [dbo].[AuthorCreate] 'Dragutin', 'Tadijanović', '19051104', NULL, 'Kada bih morala izdvojiti samo jednog najdražeg hrvatskog pjesnika, tada bi to zasigurno bio Tadijanović. Još i sada u pola noći mogu izrecitirati nekoliko njegovih pjesama koje su sve redom jedne od najboljih ikada zabilježenih na ovim prostorima. U vrijeme intelektualiziranja i pokušaja da se poezija što više „kiti“, unio je dašak svježine. Jednostavnim lirskim govorom u poeziju je donio iznimnu količinu emocija i stvarnih životnih, ali na toliko jednostavan način, razumljiv i djetetu, kakav prije nikada nije zabilježen u našoj književnosti. Svakako zavirite u njegove zbirke „Sunce nad oranicama“, „Dani djetinjstva“, „Svjetiljka ljubavi“, „Sabrane pjesme“ itd. Koju god njegovu zbirku potegnuli s police, nećete požaliti.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = 'Tin' AND [LName] = 'Ujević' AND [BirthDate] = '18910705') BEGIN
  EXECUTE [dbo].[AuthorCreate] 'Tin', 'Ujević', '18910705', NULL, 'Ujevića pamtio kao jednog od najaktivnijih hrvatskih pjesnika koji je svoja opažanja o svijetu, borbi pojedinca sa samim sobom, traženju smisla u suvremenome svijetu otuđenih ljudi pretočio u antologijske stihove. Prošao je put od traženja uzora u Matošu pa sve do Baudelairea i barokonih motiva, da bi se najbolje našao upravo u spomenutoj tematici. Oštro je odbijao pisati poeziju u funkciji patetičnog rodoljublja. Njegova najbolja djela mogu se naći u zbirkama „Lelek sebra“, „Kolajna“, „Ojađeno zvono“, „Ljudi za vratima gostionice“ i drugima. Koliko je bio značajan za našu književnosti, govori i to što je danas prema njemu nazvana i najveća pjesnička nagrada.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = 'Ivo' AND [LName] = 'Andrić' AND [BirthDate] = '18921009') BEGIN
  EXECUTE [dbo].[AuthorCreate] 'Ivo', 'Andrić', '18921009', NULL, 'Hrvatsko-srpsko-bosanskohercegovački književnik i ujedno jedini nobelovac s ovih prostora Ivo Andrić ušao je u književnost pjesmama objavljenima u „Hrvatskoj mladoj lirici“, čime se odredio kao jedan od posljednjih sljedbenika hrvatske moderne. Teške godine koje je proveo u zatvoru opisao je u svojoj zbirci „Ex ponto“, a nakon konzultantske karijere i završetka Drugog svjetskog rata potpuno se posvetio književnosti. Tada su i nastala njegova najveća djela kao što su „Prokleta avlija“ i povijesni roman „Na Drini ćupriji“, za koji je i primio Nobelovu nagradu. Nemojte propustiti pročitati barem potonji roman.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = 'Ivana' AND [LName] = 'Brlić Mažuranić' AND [BirthDate] = '18740418') BEGIN
  EXECUTE [dbo].[AuthorCreate] 'Ivana', 'Brlić Mažuranić', '18740418', NULL, 'Jedna od meni najdražih književnica i ujedno prva hrvatska književnica koja je postala članica JAZU, napisala je prave bisere dječje književnosti. „Priče iz davnine“ i „Čudnovate zgode šegrta Hlapić“ bile su neizostavan dio brojnih djetinjstva. U njihovom je pisanju autorica tražila spas od depresije od koje je bolovala, a ujedno i snažno djelovala na razvoj hrvatske dječje književnosti. Putem dječjih likova progovorila je o odnosima muškaraca i žena te napuštenoj djeci, a koliko je njezin doprinos zaista relevantan, govori i to što naši najbolji književni teoretičari poput Jože Skoka i Stjepana Hranjeca „Hlapića“ nazivaju kamenom temeljcem hrvatske dječje književnosti. Jednaki uspjeh postigle su i “Priče iz davnine”, koje su proglašene najboljom hrvatskom zbirkom umjetničkih bajki svih vremena. Dok u njima kombinira slavensku mitologiju i andersenovske elemente, obrađuje važne teme majčinstva, prirode, žetve, religije, ljudskih odnosa i pronalaska samog sebe koristeći u gotovo svakoj svojoj bajci, baš kao i u “Hlapiću”, element putovanja. No, pisala je i basne i pjesme u koje također vrijedi zaviriti.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = 'Marija' AND [LName] = 'Jurić Zagorka' AND [BirthDate] = '18730302') BEGIN
  EXECUTE [dbo].[AuthorCreate] 'Marija', 'Jurić Zagorka', '18730302', NULL, 'Ivanina suvremenica, prva hrvatska novinarka i najčitanija hrvatska književnica svih vremena također se morala nositi s brojnim poteškoćama na svome putu. Dok je Ivana patila od depresije, Marija je nailazila ne brojne prepreke u svome uspinjanju karijernom ljestvicom – i to samo zato što je bila žena. Zato je pokrenula i Ženski list, prvi hrvatski ženski časopis, te neumorno radila na svojim djelima. Pisala je komedije, jednočinke i satire, no najpoznatija je po svojim povijesnim romanima kao što su „Gordana“, „Grička vještica“ (ciklus od sedam romana), „Kći Lotrščaka“, „Kraljica Hrvata“… Iako su se neki književnici poput Matoša i Gjalskog bunili protiv nje i govorili da piše „šund za kravarice“, činjenica je da im samo nije bila po volji jer je žena. Bila je daleko ispred svog vremena i to joj mnogi „velikani“ nisu mogli oprostiti. Budete li u prilici, svakako zavirite u njezin stan na zagrebačkom Dolcu u kojem se sada nalazi Centar za ženske studije koji održava Zagorkinu ostavštinu i utječe na percepciju žena u kulturi i svakodnevnom životu.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = 'Vesna' AND [LName] = 'Parun' AND [BirthDate] = '19220410') BEGIN
  EXECUTE [dbo].[AuthorCreate] 'Vesna', 'Parun', '19220410', NULL, 'Pjesnikinja, esejistica i dramska autorica Druge moderne koju se često označavalo kao „žensku“ pjesnikinju, čime ju se opet nastojalo degradirati, svojim je radom pokazala da je itekako jedna od ponajboljih koje je naša književnost imala. Pišući o snažnim osjećajima svojstvenim snažnoj ženi, razmišljanjima o vječnim zagonetkama života i smrti, smislu postojanja, ali i prirodi, razvila je široku lepezu motiva. Ipak, do danas je ostala najupamćenija po svojoj ljubavnoj poeziji, a posebno po pjesmi „Ti koja imaš nevinije ruke“. No, koju god njezinu pjesmu primite u ruke, nećete požaliti.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = 'Kristian' AND [LName] = 'Novak' AND [BirthDate] = '19981019') BEGIN
  EXECUTE [dbo].[AuthorCreate] 'Kristian', 'Novak', '19981019', NULL, 'Prvi suvremeni književnik na našoj listi ujedno je i trenutno najčitaniji hrvatski autor. Mnogi ga prozivaju i novim Krležom, iako sam za sebe kaže da se baš nikada ne bi usporedio s njime. Ne samo da niže uspjeh za uspjehom, da za njegove naslove postoje liste čekanja u knjižnicama te da je gotovo nemoguće doći do karata za predstave napravljene prema njegovim romanima, nego se prema njegovim predlošcima snimaju i filmovi i radio-drame. Osim toga, jedan je od najprizemljenijih autora s kojima sam imala priliku razgovarati. No, što je toliko magično u njegovim djelima? Prije svega, Novak piše o onome što i sam poznaje. I u „Črnoj mati zemli“ i u „Ciganinu, ali najljepšem“ aktualizira naš međimurski kraj, svoj zavičaj, te daje realan pogled u kajkavsko društvo i suživot s Romima, bez uljepšavanja. Istodobno nevjerojatno spretno upliće obične ljudske sudbine, ponekad potresne, no uvijek humane. Spretno kombinira različite pripovjedačke tehnike, kajkavštinu i hrvatski standardni jezik. Iza njegovih romana stoje godine istraživanja jer čitateljima ne želi pružiti ništa manje nego ono najbolje što im može dati u danom trenutku. Ako ove godine posegnete makar i za jednom jedinom knjigom, neka to bude Novakova.', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Authors] WHERE [FName] = 'Slavenka' AND [LName] = 'Drakulić' AND [BirthDate] = '19490704') BEGIN
  EXECUTE [dbo].[AuthorCreate] 'Slavenka', 'Drakulić', '19490704', NULL, 'Popis završavamo jednom od najčitanijih suvremenih književnica, čija su djela prevedena na više od dvadeset jezika. Istaknuta feministica, ujedno i autorica kultne knjige hrvatskog feminizma „Smrtni grijesi feminizma“, često se okreće ženi kao središnjem motivu, kombinirajući fikciju s autobiografskim elementima. No, također poseže i za ratnim motivima, ali i ličnostima slavnih žena. Primjerice, njezini romani „Frida ili o boli“ te najnoviji „Mileva Einstein: teorija tuge“ dat će vam potpuno novi pogled na ove iznimne žene i pokazati koliku snagu krije naizgled krhki ženski duh i tijelo.', 1
END
GO

-- PUBLISHERS

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = 'Alfa') BEGIN
  EXECUTE [dbo].[PublisherCreate] 'Alfa', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = 'Globus') BEGIN
  EXECUTE [dbo].[PublisherCreate] 'Globus', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = 'Hrvatska akademija znanosti i umjetnosti') BEGIN
  EXECUTE [dbo].[PublisherCreate] 'Hrvatska akademija znanosti i umjetnosti', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = 'Matica hrvatska') BEGIN
  EXECUTE [dbo].[PublisherCreate] 'Matica hrvatska', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = 'Školska knjiga') BEGIN
  EXECUTE [dbo].[PublisherCreate] 'Školska knjiga', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = 'Večernji list') BEGIN
  EXECUTE [dbo].[PublisherCreate] 'Večernji list', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = 'Znanje') BEGIN
  EXECUTE [dbo].[PublisherCreate] 'Znanje', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = 'Vjesnik') BEGIN
  EXECUTE [dbo].[PublisherCreate] 'Vjesnik', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = 'Mozaik') BEGIN
  EXECUTE [dbo].[PublisherCreate] 'Mozaik', 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Publishers] WHERE [Name] = 'Nova knjiga') BEGIN
  EXECUTE [dbo].[PublisherCreate] 'Nova knjiga', 1
END
GO


-- BOOKS
IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = '978-9531-42585-8') BEGIN
  DECLARE @Authors AS [dbo].[Ids]
  INSERT INTO @Authors VALUES (1)
  EXECUTE [dbo].[BookCreate] '978-9531-42585-8', 'Posljednji Stipančići', NULL, 'U Posljednjim Stipančićima, jednom od najboljih i najznačajnih romana hrvatskog realizma, Vjenceslav Novak s jedne strane opisuje društveni i javni život Senja u prvoj polovici XIX. stoljeća, koji obilježavaju propadanje pomorske trgovine, klasna i politička previranja te osobito rađanje ilirskih ideja, a s druge privatni život obitelji Stipančić, s naglaskom na muško-ženske odnose. Politički život Senja prikazan je s gledišta muškarca, Ante Stipančića, a privatni sa ženskoga gledišta, koje dijele majka i kći, Valpurga i Lucija Stipančić.', 1, 9, 208, 99.00, 10.00, 30, NULL, @Authors, 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = '978-8663-69135-3') BEGIN
  DECLARE @Authors AS [dbo].[Ids]
  INSERT INTO @Authors VALUES (1)
  EXECUTE [dbo].[BookCreate] '978-8663-69135-3', 'Gospoda Glembajevi', NULL, 'Drama je podijeljena u 3 čina, a bavi se događanjima i raskolom unutar obitelji Glembay. Gospoda Glembajevi je prva od tri drame iz glembajevskog ciklusa u koji spadaju i drame U agoniji i Leda. Radnja se zbiva u Zagrebu, u kući Ignjata Glembaja u noći nakon proslave jubileja banke Glembay Ltd. 1913. godine.', 1, 10, 164, 40.00, 4.00, 5, NULL, @Authors, 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Books] WHERE [ISBN] = '978-9531-40963-6') BEGIN
  DECLARE @Authors AS [dbo].[Ids]
  INSERT INTO @Authors VALUES (6)
  EXECUTE [dbo].[BookCreate] '978-9531-40963-6', 'Čudnovate zgode šegrta Hlapića', NULL, 'Hlapić je bio dječak koji je radio kao šegrt kod majstora Mrkonje i njegove žene. Majstor se prema njemu nije lijepo odnosio, pa je Hlapić odlučio otići. Na svom putu 6 dana i 7 noći, upoznao je mnogo zanimljivih ljudi i životinja, a i zaljubio se u Gitu koja je tražila svoje roditelje. Usput, upoznao je starog štakora koji je sa svojim prijateljem ktao po selu, a Hlapiću već prvu noć ukrao čizmice. Na kraju putovanja, Hlapić je odveo Gitu svojoj kući majstoru i majstorici, za koje se na kraju saznalo da su Gitini pravi roditelji.', 1, 9, 150, 79.90, 9.00, 15, NULL, @Authors, 1
END
GO

-- BOOKSTORES

IF NOT EXISTS (SELECT ALL * FROM [dbo].[BookStores] WHERE [OIB] = '09195262064' AND [Email] = 'knjizara.jez@gmail.com') BEGIN
  INSERT INTO [dbo].[BookStores] 
  (
    [CreatedBy],
    [UpdatedBy],
    [Name],
    [OIB],
    [DelayPricePerDay],
    [Email]
  )
  VALUES 
  (
    1,
    1,
    'Knjižara Jež',
    '09195262064',
    1,
    'knjizara.jez@gmail.com'
  )
END
GO

-- BRANCHOFFICES

IF NOT EXISTS (SELECT ALL * FROM [dbo].[BranchOffices] WHERE [Name] = 'Ježev brlog' AND [Address] = 'Ilica 127, Zagreb') BEGIN
  EXECUTE [dbo].[BranchOfficeCreate] 'Ježev brlog', 'Ilica 127, Zagreb', '040855525', 'knjizara.jez@gmail.com', 1
END
GO

-- LOANS

SELECT SCOPE_IDENTITY() AS [ID]

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Loans] WHERE [BookFK] = 1 AND [UserFK] = 4) BEGIN
  DECLARE @PlannedReturnDate AS smalldatetime = DATEADD(DAY, 10, GETDATE())
  EXECUTE [dbo].[LoanLoan] 1, 4, @PlannedReturnDate, 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Loans] WHERE [BookFK] = 2 AND [UserFK] = 5) BEGIN
  DECLARE @PlannedReturnDate AS smalldatetime = DATEADD(DAY, 15, GETDATE())
  EXECUTE [dbo].[LoanLoan] 2, 5, @PlannedReturnDate, 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Loans] WHERE [BookFK] = 3 AND [UserFK] = 8) BEGIN
  DECLARE @PlannedReturnDate AS smalldatetime = DATEADD(DAY, 20, GETDATE())
  EXECUTE [dbo].[LoanLoan] 3, 8, @PlannedReturnDate, 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Loans] WHERE [BookFK] = 3 AND [UserFK] = 6) BEGIN
  DECLARE @PlannedReturnDate AS smalldatetime = DATEADD(DAY, 3, GETDATE())
  EXECUTE [dbo].[LoanLoan] 3, 6, @PlannedReturnDate, 1
END
GO

IF NOT EXISTS (SELECT ALL * FROM [dbo].[Loans] WHERE [BookFK] = 2 AND [UserFK] = 9) BEGIN
  DECLARE @PlannedReturnDate AS smalldatetime = DATEADD(DAY, 10, GETDATE())
  EXECUTE [dbo].[LoanLoan] 2, 9, @PlannedReturnDate, 1
END
GO