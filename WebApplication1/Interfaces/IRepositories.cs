﻿namespace DoctorsApp.Interfaces
{
    public interface IRepositories<K,T>
    {
        public Task<T> GetAsync(K key);
        public Task<List<T>> GetAsync();
        public Task<T> Add(T item);
        public Task<T> Update(T item);
        public Task<T> Delete(K key);
    }
}
