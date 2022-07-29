﻿using Cache.API.Entities;

namespace Cache.API.Interface
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetSubjects();
        Subject GetSubject(int id);
        void CreateSubject(Subject subject);
        Subject UpdateSubject(Subject subject);
        void DeleteSubject(int id);
    }
}
