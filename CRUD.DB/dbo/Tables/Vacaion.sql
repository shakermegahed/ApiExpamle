CREATE TABLE [dbo].[Vacaion] (
    [VacancyId]        UNIQUEIDENTIFIER NOT NULL,
    [SubmissionDate]   DATETIME         NULL,
    [EmployeeName]     NVARCHAR (50)    NULL,
    [Title]            NVARCHAR (50)    NULL,
    [Department]       INT              NULL,
    [VacationDateFrom] DATETIME         NULL,
    [VacationDateTo]   DATETIME         NULL,
    [Returning]        DATETIME         NULL,
    [Notes]            NVARCHAR (50)    NULL,
    CONSTRAINT [PK_Vacaion] PRIMARY KEY CLUSTERED ([VacancyId] ASC)
);

