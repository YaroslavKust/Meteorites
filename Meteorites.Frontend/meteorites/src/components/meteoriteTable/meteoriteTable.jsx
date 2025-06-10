import React, { useCallback, useState } from 'react';
import { useClasses, useYears, useMeteorites } from '../../queries/meteorites';

import './meteoriteTable.css';

const SortNames = {
  year: "Year",
  count: "Count",
  totalMass: "TotalMass"
}

export default function MeteoriteTable() {
  const [filters, setFilters] = useState({
    startYear: '',
    endYear: '',
    recClass: '',
    searchQuery: ''
  });
  
  const [sortOptions, setSortOptions] = useState({
    orderBy: SortNames.year,
    descending: false
  });

  const { data: years } = useYears();

  const { data: classes } = useClasses();

  const { 
    data: meteoritesData,
    isLoading, 
    isError, 
    error 
  } = useMeteorites(filters, sortOptions);

  const onSort = (orderBy) => {
    setSortOptions(prev => ({
      orderBy,
      descending: prev.orderBy === orderBy ? !prev.descending : false
    }));
  };

  const onFilterChange = (e) => {
    const { name, value } = e.target;
    setFilters(prev => ({ ...prev, [name]: value }));
  };

  const resetFilters = () => {
    setFilters({
      startYear: '',
      endYear: '',
      recClass: '',
      searchQuery: ''
    });
  };

  const sortSymbol = useCallback((sortColumnName) => {
    return sortOptions.orderBy === sortColumnName && (
        sortOptions.descending ? <span>&#8595;</span> : <span>&#8593;</span>
    );
  },[sortOptions]);

  if (isError) {
    return (
      <div className="meteorite-table__error">
        Error: {error.message}
      </div>
    );
  }

  return (
    <section className="meteorite-table">
      <h1>Метеориты</h1>
      
      <div className="meteorite-table__filters">
        <fieldset className='meteorite-table__dates-fieldset'>
          <legend>Год</legend>
          <div className="meteorite-table__filter-group">
            <label htmlFor="startYear">От</label>
            <select 
              id="startYear" 
              name="startYear" 
              value={filters.startYear} 
              onChange={onFilterChange}
            >
              <option value="">Все</option>
              {years?.map(year => (
                <option key={`from-${year}`} value={year}>{year}</option>
              ))}
            </select>
          </div>

          <div className="meteorite-table__filter-group">
            <label htmlFor="endYear">До</label>
            <select 
              id="endYear" 
              name="endYear" 
              value={filters.endYear} 
              onChange={onFilterChange}
            >
              <option value="">Все</option>
              {years?.map(year => (
                <option 
                  key={`to-${year}`}
                  value={year}
                >
                  {year}
                </option>
              ))}
            </select>
          </div>
        </fieldset>

        <div className="meteorite-table__filter-group">
          <label htmlFor="recClass">Класс:</label>
          <select 
            id="recClass" 
            name="recClass" 
            value={filters.recClass} 
            onChange={onFilterChange}
          >
            <option value="">Все</option>
            {classes?.map(cls => (
              <option key={cls} value={cls}>{cls}</option>
            ))}
          </select>
        </div>

        <div className="meteorite-table__filter-group">
          <label htmlFor="searchQuery">Название:</label>
          <input 
            type="text" 
            id="searchQuery" 
            name="searchQuery"
            value={filters.searchQuery} 
            onChange={onFilterChange}
          />
        </div>

        <button onClick={resetFilters} className="meteorite-table__reset-button">
          Сбросить фильтры
        </button>
      </div>

      { isLoading ? (
        <div className="meteorite-table__loading">Загрузка...</div>
      ) : (
          <table className="meteorite-table__table">
            <thead>
              <tr>
                <th onClick={() => onSort(SortNames.year)}>
                  Год {sortSymbol(SortNames.year)}
                </th>
                <th onClick={() => onSort(SortNames.count)}>
                  Количество {sortSymbol(SortNames.count)}
                </th>
                <th onClick={() => onSort(SortNames.totalMass)}>
                  Масса {sortSymbol(SortNames.totalMass)}
                </th>
              </tr>
            </thead>
            <tbody>
              {meteoritesData?.length > 0 ? (
                meteoritesData.map((item, index) => (
                  <tr key={index}>
                    <td>{item.year}</td>
                    <td>{item.count}</td>
                    <td>{item.totalMass}</td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan="3" style={{ textAlign: 'center' }}>Нет данных </td>
                </tr>
              )}
            </tbody>
          </table>
      )}
    </section>
  );
};