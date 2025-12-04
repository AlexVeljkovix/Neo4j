import { Fragment, useState } from "react";
import { Listbox, Transition } from "@headlessui/react";
import { CheckIcon, ChevronUpDownIcon } from "@heroicons/react/20/solid";

const MultipleSelect = ({ mechanics, selected, setSelected }) => {
  return (
    <Listbox value={selected} onChange={setSelected} multiple>
      <div className="relative mt-1">
        <Listbox.Button className="relative w-full cursor-pointer rounded-md bg-white py-2 pl-3 pr-10 text-left border border-gray-700 shadow-sm focus:outline-gray-800 focus:ring-1  sm:text-sm">
          <span className="block truncate">
            {selected.length === 0
              ? "Izaberite mehanike..."
              : selected.map((s) => s.name).join(", ")}
          </span>

          <span className="absolute inset-y-0 right-0 flex items-center pr-3">
            <ChevronUpDownIcon className="h-5 w-5 text-gray-400" />
          </span>
        </Listbox.Button>

        <Transition
          as={Fragment}
          leave="transition ease-in duration-100"
          leaveFrom="opacity-100"
          leaveTo="opacity-0"
        >
          <Listbox.Options className="absolute mt-1 max-h-60 w-full overflow-auto rounded-md bg-white shadow-lg border border-gray-200 py-1 text-base z-50">
            {mechanics.map((mechanic) => (
              <Listbox.Option
                key={mechanic.id}
                className={({ active }) =>
                  `cursor-pointer select-none relative py-2 pl-10 pr-4 ${
                    active ? "bg-gray-100" : ""
                  }`
                }
                value={mechanic}
              >
                {({ selected }) => (
                  <>
                    <span
                      className={`block truncate ${
                        selected ? "font-medium" : "font-normal"
                      }`}
                    >
                      {mechanic.name}
                    </span>

                    {selected && (
                      <span className="absolute inset-y-0 left-0 flex items-center pl-2 text-gray-800">
                        <CheckIcon className="h-5 w-5" />
                      </span>
                    )}
                  </>
                )}
              </Listbox.Option>
            ))}
          </Listbox.Options>
        </Transition>
      </div>
    </Listbox>
  );
};

export default MultipleSelect;
