using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel input)
        {
            if (!IsTrainerExist(input.TrainerId)
                || !IsCategoryExist(input.CategoryId)
                || !IsValidDateRange(input.StartDate, input.EndDate))
                return false;

            var session = _mapper.Map<CreateSessionViewModel, Session>(input);
            _unitOfWork.GetRepository<Session>().Add(session);

            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.SessionRepository
                            .GetAllSessionsWithTrainerAndCategory()
                            .OrderByDescending(x => x.StartDate);

            if (sessions is null || !sessions.Any())
                return [];

            var mappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);

            foreach (var session in mappedSessions)
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);

            return mappedSessions;
        }

        public SessionViewModel? GetSessionById(int sessionId)
        {
            var session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(sessionId);

            if (session is null) return null;

            var mappedSession = _mapper.Map<Session, SessionViewModel>(session);
            mappedSession.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(sessionId);

            return mappedSession;
        }

        public UpdateSessionViewModel? GetSessionToUpdate(int sessionId)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
            if (session is null) 
                return null;

            return _mapper.Map<UpdateSessionViewModel>(session);
        }

        public bool UpdateSession(int sessionId, UpdateSessionViewModel input)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);

            if (!IsSessionAvailableForUpdate(session)) 
                return false;
            if (!IsTrainerExist(input.TrainerId)) 
                return false;
            if (!IsValidDateRange(input.StartDate, input.EndDate)) 
                return false;

            session.TrainerId = input.TrainerId;
            session.StartDate = input.StartDate;
            session.EndDate = input.EndDate;
            session.Description = input.Description;
            session.UpdatedAt = DateTime.Now;  

            _unitOfWork.GetRepository<Session>().Update(session);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool RemoveSession(int sessionId)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);

            if (!IsSessionAvailableForRemove(session))
                return false;

            _unitOfWork.GetRepository<Session>().Delete(session);
            return _unitOfWork.SaveChanges() > 0;
        }
        public IEnumerable<CategorySelectViewModel> GetCategoriesDropDown()
        {
            var categories = _unitOfWork.GetRepository<Category>().GetAll();
            return _mapper.Map<IEnumerable<CategorySelectViewModel>>(categories);
        }
        public IEnumerable<TrainerSelectViewModel> GetTrainersDropDown()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(trainers);
        }


        #region Helper Methods
        private bool IsTrainerExist(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            return trainer is null ? false : true;
        }
        private bool IsCategoryExist(int categoryId)
        {
            var category = _unitOfWork.GetRepository<Category>().GetById(categoryId);
            return category is null ? false : true;
        }
        private bool IsValidDateRange(DateTime startDate, DateTime endDate)
        {
            return endDate > startDate && startDate > DateTime.Now;
        }
        private bool IsSessionAvailableForUpdate(Session session)
        {
            if (session is null) 
                return false;
            if (session.EndDate < DateTime.Now) 
                return false;
            if (session.StartDate <= DateTime.Now) 
                return false;

            var hasActiveBookings = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);
            if (hasActiveBookings > 0) 
                return false;

            return true;
        }
        private bool IsSessionAvailableForRemove(Session session)
        {
            if (session is null) 
                return false;
            if (session.StartDate > DateTime.Now) 
                return false;
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) 
                return false;

            var hasActiveBookings = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);
            if (hasActiveBookings > 0) 
                return false;

            return true;
        }

       
        #endregion
    }
}