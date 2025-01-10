namespace ModularMonolith.Modules.Ordering.Persistance.WriteModel
{

    using Domain;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DocumentNumberGenerator(WriteDbContext dbContext) : IDocumentNumberGenerator
    {
        private readonly WriteDbContext dbContext = dbContext;
        protected readonly DbSet<DocumentNumber> documentNumberTable = dbContext.Set<DocumentNumber>();

        public async Task<string> CreateDocumentNumberAsync(EntityType entityType, DateTime dateTime, CancellationToken cancellationToken)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            DocumentNumber? documentNumber = await documentNumberTable.SingleOrDefaultAsync(n => n.EntityTypeId == entityType.Code && n.Year == dateTime.Year && n.Month == dateTime.Month && n.Day == dateTime.Day, cancellationToken);
            if (documentNumber == null)
            {
                documentNumber = DocumentNumber.Create(entityType.Code, dateTime, 0);
                await documentNumberTable.AddAsync(documentNumber, cancellationToken);
            }
            documentNumber.Increase();
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return documentNumber.GetNumber();
        }
    }
}