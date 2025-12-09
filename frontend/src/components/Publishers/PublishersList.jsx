import React from "react";
import PublisherCard from "./PublisherCard";

const PublishersList = ({ publishers }) => {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
      {publishers.map((publisher) => (
        <div
          key={publisher.id}
          className="bg-gray-800 rounded-lg shadow-md p-4 text-white hover:bg-gray-700 transition"
        >
          <PublisherCard publisher={publisher} />
        </div>
      ))}
    </div>
  );
};

export default PublishersList;
