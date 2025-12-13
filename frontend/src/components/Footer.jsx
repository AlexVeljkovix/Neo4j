import React from "react";
import { Link } from "react-router-dom";

function classNames(...classes) {
  return classes.filter(Boolean).join(" ");
}

const Footer = () => {
  const currentYear = new Date().getFullYear();

  return (
    <footer className="relative bg-gray-800 after:pointer-events-none after:absolute after:inset-x-0 after:bottom-0 after:h-px after:bg-white/10">
      <div className="w-full mx-auto max-w-7xl p-4 md:flex md:items-center md:justify-between">
        <span className="text-gray-300 rounded-md px-3 py-2 text-sm font-medium">
          {`© ${currentYear} . All Rights Reserved.`}
        </span>
        <ul className="flex flex-wrap items-center mt-3 text-sm font-medium text-body sm:mt-0">
          <li>
            <Link
              to="/about"
              className="text-gray-300 hover:bg-white/5 hover:text-white
                rounded-md px-3 py-2 text-sm font-medium"
            >
              About
            </Link>
          </li>
          <li>
            <Link
              to="/privacy"
              className="text-gray-300 hover:bg-white/5 hover:text-white
                rounded-md px-3 py-2 text-sm font-medium"
            >
              Privacy Policy
            </Link>
          </li>
          <li>
            <Link
              to="/licensing"
              className="text-gray-300 hover:bg-white/5 hover:text-white
                rounded-md px-3 py-2 text-sm font-medium"
            >
              Licensing
            </Link>
          </li>
          <li>
            <Link
              to="/contact"
              className="text-gray-300 hover:bg-white/5 hover:text-white
                rounded-md px-3 py-2 text-sm font-medium"
            >
              Contact
            </Link>
          </li>
        </ul>
      </div>
    </footer>
  );
};

export default Footer;
