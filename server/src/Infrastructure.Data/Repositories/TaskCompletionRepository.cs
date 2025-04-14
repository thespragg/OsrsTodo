using Domain.Contracts.Repositories;
using Domain.Entities;
using Infrastructure.Data.Mappers;
using Infrastructure.Data.Schema;
using Infrastructure.Data.Schema.Models;

namespace Infrastructure.Data.Repositories;

internal sealed class TaskCompletionRepository(
    OsrsTodoDbContext db
) : BaseRepository<TaskCompletionDataModel, TaskCompletionEntity, TaskCompletionMapper>(db), ITaskCompletionRepository;