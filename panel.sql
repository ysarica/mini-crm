USE [PanelDB]
GO
/****** Object:  Table [dbo].[TBLYetki]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLYetki](
	[YetkiID] [int] IDENTITY(1,1) NOT NULL,
	[YetkiAd] [nvarchar](150) NULL,
	[YetkiIcon] [nvarchar](150) NULL,
 CONSTRAINT [PK_TBLYetki] PRIMARY KEY CLUSTERED 
(
	[YetkiID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLTalepMesaj]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLTalepMesaj](
	[TMID] [int] IDENTITY(1,1) NOT NULL,
	[TalepID] [int] NULL,
	[Mesaj] [nvarchar](max) NULL,
	[Ek] [nvarchar](150) NULL,
	[Tarih] [nvarchar](150) NULL,
	[Okundu] [nvarchar](50) NULL,
	[GonderenID] [int] NULL,
 CONSTRAINT [PK_TBLTalepMesaj] PRIMARY KEY CLUSTERED 
(
	[TMID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLTalep]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLTalep](
	[TalepID] [int] IDENTITY(1,1) NOT NULL,
	[GonderenAdi] [nvarchar](150) NULL,
	[GonderenID] [int] NULL,
	[TalepTip] [nvarchar](50) NULL,
	[OlusturulmaTarihi] [nvarchar](150) NULL,
	[HizmetKategoriID] [int] NULL,
	[HizmetKategoriAdi] [nvarchar](150) NULL,
	[HizmetID] [int] NULL,
	[Talep] [nvarchar](max) NULL,
	[Logo] [nvarchar](150) NULL,
	[FirmaAdi] [nvarchar](150) NULL,
	[AdSoyad] [nvarchar](150) NULL,
	[Telefon] [nvarchar](150) NULL,
	[Mail] [nvarchar](150) NULL,
	[Okundu] [nvarchar](50) NULL,
	[Durum] [nvarchar](50) NULL,
	[Silindi] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBLTalep] PRIMARY KEY CLUSTERED 
(
	[TalepID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLSoru]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLSoru](
	[SID] [int] NULL,
	[Soru] [nchar](10) NULL,
	[CevapTipi] [nchar](10) NULL,
	[FormID] [nchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLSohbet]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLSohbet](
	[SohbetID] [int] IDENTITY(1,1) NOT NULL,
	[MesajID] [int] NULL,
	[Mesaj] [nvarchar](max) NULL,
	[Ek] [nvarchar](150) NULL,
	[Tarih] [nvarchar](50) NULL,
	[GonderenID] [int] NULL,
	[GonderenAd] [nvarchar](150) NULL,
	[AliciID] [int] NULL,
	[AliciAd] [nvarchar](150) NULL,
	[Okundu] [nvarchar](50) NULL,
	[Silindi] [nvarchar](50) NULL,
	[DosyaTipi] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBLSohbet] PRIMARY KEY CLUSTERED 
(
	[SohbetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLOdemeYontem]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLOdemeYontem](
	[OYID] [int] IDENTITY(1,1) NOT NULL,
	[Yontem] [nvarchar](150) NULL,
 CONSTRAINT [PK_TBLOdemeYontem] PRIMARY KEY CLUSTERED 
(
	[OYID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLOdemeBildirim]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLOdemeBildirim](
	[OBID] [int] IDENTITY(1,1) NOT NULL,
	[HizmetID] [int] NULL,
	[MusteriID] [int] NULL,
	[OdemeYontem] [nvarchar](150) NULL,
	[Tutar] [nvarchar](50) NULL,
	[Tarih] [nvarchar](50) NULL,
	[OdemeBanka] [nvarchar](150) NULL,
	[AdSoyad] [nvarchar](50) NULL,
	[Durum] [nvarchar](50) NULL,
	[Silindi] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBLOdemeBildirim] PRIMARY KEY CLUSTERED 
(
	[OBID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLOdeme]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLOdeme](
	[OdemeID] [int] IDENTITY(1,1) NOT NULL,
	[HizmetID] [int] NULL,
	[MusteriID] [int] NULL,
	[HizmetKategoriID] [int] NULL,
	[TemsilciID] [int] NULL,
	[HizmetAdi] [nvarchar](150) NULL,
	[MusteriAdi] [nvarchar](150) NULL,
	[FirmaAdi] [nvarchar](150) NULL,
	[HizmetKategoriAd] [nvarchar](150) NULL,
	[AlınanOdeme] [nvarchar](150) NULL,
	[OdemeTipi] [nvarchar](150) NULL,
	[OdemeYontem] [nvarchar](150) NULL,
	[OdemeTarihi] [nvarchar](150) NULL,
 CONSTRAINT [PK_TBLOdeme] PRIMARY KEY CLUSTERED 
(
	[OdemeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLNotlar]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLNotlar](
	[NotID] [int] IDENTITY(1,1) NOT NULL,
	[NotAciklama] [nvarchar](150) NULL,
	[NotTür] [nvarchar](50) NULL,
	[KullaniciID] [int] NULL,
 CONSTRAINT [PK_TBLNotlar] PRIMARY KEY CLUSTERED 
(
	[NotID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLMesajTip]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLMesajTip](
	[MTipID] [int] IDENTITY(1,1) NOT NULL,
	[MTipAdi] [nvarchar](150) NULL,
 CONSTRAINT [PK_TBLMesajTip] PRIMARY KEY CLUSTERED 
(
	[MTipID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLMesaj]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLMesaj](
	[MesajID] [int] IDENTITY(1,1) NOT NULL,
	[Konu] [nvarchar](max) NULL,
	[Tarih] [nvarchar](150) NULL,
	[GonderenAdi] [nvarchar](150) NULL,
	[GonderenID] [int] NULL,
	[AliciAdi] [nvarchar](150) NULL,
	[AliciID] [int] NULL,
	[MTipAdi] [nvarchar](150) NULL,
	[MTipID] [int] NULL,
	[Silindi] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBLMesaj] PRIMARY KEY CLUSTERED 
(
	[MesajID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLKullaniciLog]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLKullaniciLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciID] [int] NULL,
	[KullaniciAdi] [nvarchar](150) NULL,
	[İslem] [nvarchar](max) NULL,
	[Tarih] [nvarchar](150) NULL,
 CONSTRAINT [PK_TBLKullaniciLog] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLKullanici]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLKullanici](
	[KullaniciID] [int] IDENTITY(1,1) NOT NULL,
	[AdSoyad] [nvarchar](150) NULL,
	[KullaniciTip] [nvarchar](150) NULL,
	[Telefon] [nvarchar](150) NULL,
	[Mail] [nvarchar](150) NULL,
	[Adres] [nvarchar](max) NULL,
	[Resim] [nvarchar](150) NULL,
	[Sifre] [nvarchar](150) NULL,
	[KullaniciTipID] [int] NULL,
	[TemsilciAdi] [nvarchar](150) NULL,
	[TemsilciID] [int] NULL,
	[Tarih] [nvarchar](50) NULL,
	[Aciklama] [nvarchar](max) NULL,
	[Firma] [nvarchar](150) NULL,
	[Durum] [nvarchar](50) NULL,
	[il] [nvarchar](150) NULL,
	[ilce] [nvarchar](150) NULL,
	[DogumTarihi] [nvarchar](150) NULL,
 CONSTRAINT [PK_TBLKullanici] PRIMARY KEY CLUSTERED 
(
	[KullaniciID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLIslemler]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLIslemler](
	[IslemID] [int] IDENTITY(1,1) NOT NULL,
	[IslemAdi] [nvarchar](150) NULL,
	[HizmetID] [int] NULL,
	[HarcananZaman] [nvarchar](150) NULL,
	[Fiyat] [nvarchar](150) NULL,
	[TemsilciID] [nvarchar](150) NULL,
	[TemsilciAdi] [nvarchar](150) NULL,
	[Aciklama] [nvarchar](150) NULL,
 CONSTRAINT [PK_TBLIslemler] PRIMARY KEY CLUSTERED 
(
	[IslemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLHizmetMesaj]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLHizmetMesaj](
	[HMID] [int] IDENTITY(1,1) NOT NULL,
	[HizmetID] [int] NULL,
	[Mesaj] [nvarchar](max) NULL,
	[Ek] [nvarchar](150) NULL,
	[Tarih] [nvarchar](150) NULL,
	[Okundu] [nvarchar](50) NULL,
	[GonderenID] [int] NULL,
 CONSTRAINT [PK_TBLHizmetMesaj] PRIMARY KEY CLUSTERED 
(
	[HMID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLHizmetKategori]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLHizmetKategori](
	[HKategoriID] [int] IDENTITY(1,1) NOT NULL,
	[KategoriAd] [nvarchar](150) NULL,
	[Icon] [nvarchar](150) NULL,
 CONSTRAINT [PK_TBLHizmetKategori] PRIMARY KEY CLUSTERED 
(
	[HKategoriID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLHizmet]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLHizmet](
	[HizmetID] [int] IDENTITY(1,1) NOT NULL,
	[MüsteriID] [int] NULL,
	[MüsteriAdi] [nvarchar](150) NULL,
	[HizmetAdi] [nvarchar](150) NULL,
	[BaslangicTarihi] [nvarchar](150) NULL,
	[BitisTarihi] [nvarchar](150) NULL,
	[HizmetKategoriAd] [nvarchar](150) NULL,
	[HizmetKategoriID] [int] NULL,
	[TemsilciID] [int] NULL,
	[TemsilciAdi] [nvarchar](150) NULL,
	[FTPSunucu] [nvarchar](150) NULL,
	[FTPKullaniciAdi] [nvarchar](150) NULL,
	[FTPSifre] [nvarchar](150) NULL,
	[HizmetBedeli] [nvarchar](150) NULL,
	[Aciklama] [nvarchar](max) NULL,
	[Firma] [nvarchar](150) NULL,
	[OdemeDurum] [nvarchar](50) NULL,
	[GorulduDurum] [nvarchar](50) NULL,
	[HizmetDurum] [nvarchar](50) NULL,
	[OdemeTarihi] [nvarchar](150) NULL,
	[HizmetBaslangicTarihi] [nvarchar](150) NULL,
	[HizmetBitisTarihi] [nvarchar](150) NULL,
	[IslemTarihi] [nvarchar](150) NULL,
 CONSTRAINT [PK_TBLHizmet] PRIMARY KEY CLUSTERED 
(
	[HizmetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLHesaplar]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLHesaplar](
	[HID] [int] IDENTITY(1,1) NOT NULL,
	[BankaAdi] [nvarchar](150) NULL,
	[Iban] [nvarchar](150) NULL,
	[AdSoyad] [nvarchar](150) NULL,
	[Logo] [nvarchar](150) NULL,
 CONSTRAINT [PK_TBLHesaplar] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLFatura]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLFatura](
	[FaturaID] [int] IDENTITY(1,1000) NOT NULL,
	[HizmetID] [int] NULL,
	[KullaniciID] [int] NULL,
	[KullaniciAd] [nvarchar](150) NULL,
	[FirmaAdi] [nvarchar](150) NULL,
	[HizmetAdi] [nvarchar](150) NULL,
	[HizmetKategoriAdi] [nvarchar](150) NULL,
	[HizmetBedeli] [nvarchar](50) NULL,
	[KalanTutar] [nvarchar](50) NULL,
	[OdemeDurum] [nvarchar](150) NULL,
	[OdemeTarih] [nvarchar](50) NULL,
	[OdemeYontem] [nvarchar](50) NULL,
	[OdemeTip] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBLFatura] PRIMARY KEY CLUSTERED 
(
	[FaturaID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLDosya]    Script Date: 02/24/2023 00:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLDosya](
	[DID] [int] IDENTITY(1,1) NOT NULL,
	[DosyaYolu] [nvarchar](150) NULL,
	[DosyaTipi] [nvarchar](50) NULL,
	[DosyaAdi] [nvarchar](150) NULL,
	[DosyaAciklama] [nvarchar](max) NULL,
	[YukleyenAdi] [nvarchar](150) NULL,
	[YukleyenID] [int] NULL,
	[SahiplikTipi] [nvarchar](150) NULL,
	[SahiplikID] [int] NULL,
	[Tarih] [nvarchar](50) NULL,
 CONSTRAINT [PK_TBLDosya] PRIMARY KEY CLUSTERED 
(
	[DID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
