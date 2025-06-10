import { useQuery } from '@tanstack/react-query';
import { API_BASE_URL } from '../constants/apiUrl';

async function fetchData(endpoint, params = {}) {
  const url = new URL(`${API_BASE_URL}/${endpoint}`);

  Object.entries(params).forEach(([key, value]) => {
    if (value) {
      url.searchParams.append(key, value);
    }
  });

  const response = await fetch(url);

  if (!response.ok) {
    throw new Error(`HTTP error! status: ${response.status}`);
  }

  return response.json();
}

const useYears = () => useQuery({
  queryKey: ['years'], 
  queryFn: async () => await fetchData('years')
});

const useClasses = () => useQuery({
  queryKey: ['rec-classes'],
  queryFn: async () => await fetchData('rec-classes')
}); 

const useMeteorites = (filters, sortOptions) => 
  useQuery({
      queryKey: ['meteorites', filters, sortOptions],
      queryFn: async () => await fetchData(
        'meteorites', 
        {
            StartYear: filters.startYear,
            EndYear: filters.endYear,
            RecClass: filters.recClass,
            SearchQuery: filters.searchQuery,
            OrderBy: sortOptions.orderBy,
            Descending: sortOptions.descending
        }),
      staleTime: 10 * 60 * 1000
    }
  );

export {
    useClasses,
    useYears,
    useMeteorites
};