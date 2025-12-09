export default function SearchBar() {
  return (
    <div>
      <div className="sm:w-48 mt-2">
        <div className="flex items-center rounded-md bg-white/5 pl-3 outline-1 -outline-offset-1 outline-gray-600 has-[input:focus-within]:outline-2 has-[input:focus-within]:-outline-offset-2 has-[input:focus-within]:outline-indigo-500">
          <input
            id="search"
            name="search"
            type="text"
            placeholder="Search"
            className="block min-w-0 grow bg-gray-800 py-1.5 pr-3 pl-1 text-base text-white placeholder:text-gray-500 focus:outline-none sm:text-sm/6"
          />
        </div>
      </div>
    </div>
  );
}
