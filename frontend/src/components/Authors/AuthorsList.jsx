import AuthorCard from "./AuthorCard";
const AuthorsList = ({ authors }) => {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
      {authors.map((author) => (
        <div
          key={author.id}
          className="bg-gray-800 rounded-lg shadow-md p-4 text-white hover:bg-gray-700 transition"
        >
          <AuthorCard author={author} />
        </div>
      ))}
    </div>
  );
};

export default AuthorsList;
